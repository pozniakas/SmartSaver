using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DbEntities.Entities;

namespace WebAPI.Services
{
    public class BankStatmentReader
    {
        private delegate Transaction ReadRecord();
        private ReadRecord _readRecord;

        private string guessedDelimiter = string.Empty;

        private readonly StreamReader stream;
        private readonly CsvReader csv;
        private IEnumerable<string> header;
        public BankStatmentReader(StreamReader streamReader)
        {
            stream = streamReader;
            guessDelimiter();

            //if (string.IsNullOrEmpty(guessedDelimiter)) throw;

            csv = new CsvReader(stream, CultureInfo.CurrentCulture);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.MissingFieldFound = null;
            csv.Configuration.Delimiter = guessedDelimiter;
        }

        public IEnumerable<Transaction> Read()
        {
            var transactions = new List<Transaction>();

            SetHeader();
            RecognizeBank();
            /// Console.Write($"{string.Join(",", header)} \n\n");
            if (_readRecord == null)
            {
                return transactions;
            }
            Console.WriteLine($"{string.Join(",", header)} \n\n");

            while (csv.Read())
            {
                transactions.Add(_readRecord.Invoke());
                //Console.WriteLine(csv.GetField<DateTime>("Data"));
            }

            return transactions;
        }

        private void RecognizeBank()
        {
            var threeColumnString = string.Join(",", header.Take(3));
            switch (threeColumnString)
            {
                // Swedbank LT
                case "Data,Gavejas,Gavejo saskaita":
                    _readRecord = new ReadRecord(SwedbankLtReadRecord);
                    break;
                // Luminor EN
                case "Date,Time,Amount":
                    _readRecord = new ReadRecord(LuminorEnReadRecord);
                    break;
                // Luminor LT
                case "Data,Laikas,Suma":
                    _readRecord = new ReadRecord(LuminorLtReadRecord);
                    break;
                // Seb EN
                case "INSTRUCTION ID,DATE,CURRENCY":
                    _readRecord = new ReadRecord(SebEnReadRecord);
                    break;
                // Seb LT
                case "DOK NR.,DATA,VALIUTA":
                    _readRecord = new ReadRecord(SebLtReadRecord);
                    break;
            }
        }

        private void SetHeader()
        {
            csv.Read();
            csv.ReadHeader();

            header = csv.Context.HeaderRecord;
            header = header.Where(x => !string.IsNullOrEmpty(x));

            if (header.Count() <= 2)
            {
                SetHeader();
            }
            Console.WriteLine($"{string.Join("|", header)} \n\n");
        }

        private void guessDelimiter()
        {
            bool finished = false;
            while (stream.Peek() > 0 && !finished)
            {
                switch (stream.Read()) {
                    case ';': guessedDelimiter = ";"; finished = true; break;
                    case ',': guessedDelimiter = ","; finished = true; break;
                    case '|': guessedDelimiter = "|"; finished = true; break;
                    default: continue;
                }
            }

            stream.BaseStream.Position = 0;
        }

        private decimal GetAmout(decimal sum, string debitOrCredit)
        {
            return debitOrCredit.ToLower() == "d" ? -sum : sum;
        }
        private Transaction? LuminorLtReadRecord()
        {
            /// Operacijos/Balanso Tipas,Data,Laikas,Suma,Ekvivalentas,C/D,Orig. suma,Orig. valiuta,
            /// Operacijos dok. Nr.,Operacijos eilutė (identifikatorius),Kliento kodas gavėjo informac. sistemoje,
            /// Įmokos kodas,Mokėjimo paskirtis,Kitos pusės BIC,Kitos pusės Kredito įstaigos pavadinimas,Kitos pusės Sąskaitos Nr.,
            /// Kitos pusės Pavadinimas,Kitos pusės Asmens kodas/Registracijos Nr.,Kitos pusės Kliento kodas mokėtojo informacinėje sistemoje

            return csv.TryGetField("Orig. suma", out decimal sum) && csv.TryGetField("C/D", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("Data"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("Kitos pusės Pavadinimas"),
                    Details = csv.GetField("Mokėjimo paskirtis")
                }
                : null;
        }

        private Transaction LuminorEnReadRecord()
        {
            ///Transaction type,Date,Time,Amount,Equivalent,C/D,Orig. amount,Orig. currency,Document number,Transaction ID,
            ///Customer‘s code in beneficiary IS,Payment code,Payment details,Counterparty BIC,Counterparty Designation of counterparties credit institution,
            ///Counterparty Account number,	Counterparty Designation,Counterparty Reg No.,Counterparty Customer‘s code in payer IS,Ultimate Payer Account number

            return csv.TryGetField("Orig. amount", out decimal sum) && csv.TryGetField("C/D", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("Date"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("Counterparty Designation"),
                    Details = csv.GetField("Payment details")
                }
                : null;
        }

        private Transaction? SwedbankLtReadRecord()
        {
            /// Data,Gavejas,Gavejo saskaita,Paaiskinimai,Suma,Valiuta,D/K,Iraso Nr.,Kodas,Imokos kodas,
            /// Dok. Nr.,Kliento kodas moketojo IS,Kliento kodas,Pradinis moketojas,Galutinis gavejas
            return csv.TryGetField("Suma", out decimal sum) && csv.TryGetField("D/K", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("Data"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("Gavejas"),
                    Details = csv.GetField("Paaiskinimai")
                } 
                : null;
        }

        private Transaction SebEnReadRecord()
        {

            /// INSTRUCTION ID,DATE,CURRENCY,AMOUNT,COUNTERPARTY,DEBTOR/CREDITOR ID,ACCOUNT NO,
            /// CREDIT INSTITUTION NAME,CREDIT INSTITUTION SWIFT,DETAILS OF PAYMENTS,TRANSACTION CODE,
            /// DOCUMENT DATE,TRANSACTION TYPE,REFERENCE NO,DEBIT/CREDIT,AMOUNT IN ACCOUNT CURRENCY,ACCOUNT NO,ACCOUNT CURRENCY
            return csv.TryGetField("AMOUNT", out decimal sum) && csv.TryGetField("DEBIT/CREDIT", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("DATE"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("COUNTERPARTY"),
                    Details = csv.GetField("DETAILS OF PAYMENTS")
                }
                : null;
        }
        private Transaction SebLtReadRecord()
        {
            ///DOK NR.,DATA,VALIUTA,SUMA,MOKĖTOJO ARBA GAVĖJO PAVADINIMAS,MOKĖTOJO ARBA GAVĖJO IDENTIFIKACINIS KODAS,SĄSKAITA,
            ///KREDITO ĮSTAIGOS PAVADINIMAS,KREDITO ĮSTAIGOS SWIFT KODAS,MOKĖJIMO PASKIRTIS,TRANSAKCIJOS KODAS,
            ///DOKUMENTO DATA,TRANSAKCIJOS TIPAS,NUORODA,DEBETAS/KREDITAS,SUMA SĄSKAITOS VALIUTA,SĄSKAITOS NR,SĄSKAITOS VALIUTA

            return csv.TryGetField("SUMA", out decimal sum) && csv.TryGetField("DEBETAS/KREDITAS", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("DATA"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("MOKĖTOJO ARBA GAVĖJO PAVADINIMAS"),
                    Details = csv.GetField("MOKĖJIMO PASKIRTIS")
                }
                : null;
        }

    }
}
