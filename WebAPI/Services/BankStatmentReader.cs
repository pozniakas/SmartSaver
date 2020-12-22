using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DbEntities.Entities;
using System.Diagnostics;

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
            if (_readRecord == null)
            {
                return transactions;
            }
            Console.WriteLine($"{string.Join(",", header)} \n\n");

            while (csv.Read())
            {
                transactions.Add(_readRecord.Invoke());
                
            }
            transactions.RemoveAll(item => item == null); // ištrinam null reikšmes, kažkodėl atsiranda kelios SWED banko skaityme ir bent jau viena SEB :D
            var amount = transactions.First().Amount; // išsiėmam reikšmes lyginimui
            var details = transactions.First().Details;
            transactions.RemoveAt(0); 
            
            while (transactions.First().Amount != amount && transactions.First().Details != details) // del csv pradejimo skaityt po poros eiluciu vel headeri reikia isnaikinti pirmus elementus transaction'us
            {
                transactions.RemoveAt(0);
            }


            return transactions;
        }

        private void RecognizeBank()
        {
            var threeColumnString = string.Join(",", header.Take(3));
            switch (threeColumnString)
            {
                // Swedbank LT
                case "Data,Gavėjas,Gavėjo sąskaita":
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
            csv.Configuration.ShouldSkipRecord = row => row.Length == 1;
            csv.ReadHeader();

            header = csv.Context.HeaderRecord;
            header = header.Where(x => !string.IsNullOrEmpty(x));
            

            if (header.Count() <= 2)
            {            
                SetHeader();
            }

            Debug.WriteLine($"{string.Join("|", header)} \n\n");
 //           SetHeader();
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

#nullable enable
        private Transaction? LuminorLtReadRecord()
        {
            /// Operacijos/Balanso Tipas,Data,Laikas,Suma,Ekvivalentas,C/D,Orig. suma,Orig. valiuta,
            /// Operacijos dok. Nr.,Operacijos eilutė (identifikatorius),Kliento kodas gavėjo informac. sistemoje,
            /// Įmokos kodas,Mokėjimo paskirtis,Kitos pusės BIC,Kitos pusės Kredito įstaigos pavadinimas,Kitos pusės Sąskaitos Nr.,
            /// Kitos pusės Pavadinimas,Kitos pusės Asmens kodas/Registracijos Nr.,Kitos pusės Kliento kodas mokėtojo informacinėje sistemoje

            return  csv.TryGetField("Orig. suma", out string debitOrCredit) && csv.TryGetField("Ekvivalentas", out string suma) && csv.TryGetField("Laikas", out string data)
                ? new Transaction
                {
                    TrTime = DateTime.ParseExact(data, "yyyyMMdd", CultureInfo.InvariantCulture),
                    Amount = GetAmout(Convert.ToDecimal(suma, CultureInfo.InvariantCulture), debitOrCredit),
                    CounterParty = csv.GetField("Kitos pus�s S�skaitos Nr."),
                    Details = csv.GetField("Kitos pus�s BIC")
                }
                : null;
        }

        private Transaction? LuminorEnReadRecord()
        {
            ///Transaction type,Date,Time,Amount,Equivalent,C/D,Orig. amount,Orig. currency,Document number,Transaction ID,
            ///Customer‘s code in beneficiary IS,Payment code,Payment details,Counterparty BIC,Counterparty Designation of counterparties credit institution,
            ///Counterparty Account number,	Counterparty Designation,Counterparty Reg No.,Counterparty Customer‘s code in payer IS,Ultimate Payer Account number
            var a = csv.TryGetField("Orig. amount", out string adebitOrCredit);
            var b = csv.TryGetField("Equivalent", out string asuma);
            var c = csv.TryGetField("Time", out string adata);
            var d = csv.GetField("Counterparty Account number");
            var e = csv.GetField("Counterparty BIC");
            return csv.TryGetField("Orig. amount", out string debitOrCredit) 
                && csv.TryGetField("Equivalent", out string suma)
                && csv.TryGetField("Time", out string data)
                && csv.TryGetField("Amount", out decimal amount)
                ? new Transaction
                {
                    TrTime = DateTime.ParseExact(data, "yyyyMMdd", CultureInfo.InvariantCulture),
                    Amount = GetAmout(Convert.ToDecimal(suma, CultureInfo.InvariantCulture), debitOrCredit),
                    CounterParty = csv.GetField("Counterparty Account number"),
                    Details = csv.GetField("Counterparty BIC")
                }
                : null;
        }

        private Transaction? SwedbankLtReadRecord()
        {
            /// Data,Gavejas,Gavejo saskaita,Paaiskinimai,Suma,Valiuta,D/K,Iraso Nr.,Kodas,Imokos kodas,
            /// Dok. Nr.,Kliento kodas moketojo IS,Kliento kodas,Pradinis moketojas,Galutinis gavejas
            var a = csv.TryGetField("Valiuta", out string suma);
            var b = csv.TryGetField("Įrašo Nr.", out string debitOrCredita);
            var w = csv.GetField("Paaiškinimai");
            if (w == "Likutis pradžiai")
                return null;
            var c = csv.GetField<DateTime>("Gavėjas");
            var d = csv.GetField("Gavėjas");
            var e = csv.GetField("Suma");
            return csv.TryGetField("Valiuta", out string sum) && csv.TryGetField("Įrašo Nr.", out string debitOrCredit) && csv.TryGetField("Data", out decimal isasa)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("Gavėjas"),
                    Amount = GetAmout(Convert.ToDecimal(sum, CultureInfo.InvariantCulture), debitOrCredit),
                    CounterParty = csv.GetField("Gavėjo sąskaita"),
                    Details = csv.GetField("Suma")
                }
                : null;
        }

        private Transaction? SebEnReadRecord()
        {

            /// INSTRUCTION ID,DATE,CURRENCY,AMOUNT,COUNTERPARTY,DEBTOR/CREDITOR ID,ACCOUNT NO,
            /// CREDIT INSTITUTION NAME,CREDIT INSTITUTION SWIFT,DETAILS OF PAYMENTS,TRANSACTION CODE,
            /// DOCUMENT DATE,TRANSACTION TYPE,REFERENCE NO,DEBIT/CREDIT,AMOUNT IN ACCOUNT CURRENCY,ACCOUNT NO,ACCOUNT CURRENCY
            return csv.TryGetField("AMOUNT", out decimal sum) && csv.TryGetField("DEBIT/CREDIT", out string debitOrCredit)
                ? new Transaction
                {
                    TrTime = csv.GetField<DateTime>("DATE"),
                    Amount = GetAmout(sum, debitOrCredit),
                    CounterParty = csv.GetField("CURRENCY"),
                    Details = csv.GetField("DETAILS OF PAYMENTS")
                }
                : null;
        }
        private Transaction? SebLtReadRecord()
        {
            ///DOK NR.,DATA,VALIUTA,SUMA,MOKĖTOJO ARBA GAVĖJO PAVADINIMAS,MOKĖTOJO ARBA GAVĖJO IDENTIFIKACINIS KODAS,SĄSKAITA,
            ///KREDITO ĮSTAIGOS PAVADINIMAS,KREDITO ĮSTAIGOS SWIFT KODAS,MOKĖJIMO PASKIRTIS,TRANSAKCIJOS KODAS,
            ///DOKUMENTO DATA,TRANSAKCIJOS TIPAS,NUORODA,DEBETAS/KREDITAS,SUMA SĄSKAITOS VALIUTA,SĄSKAITOS NR,SĄSKAITOS VALIUTA
            var a = csv.GetField("DATA");
            var b = csv.GetField("MOKĖTOJO ARBA GAVĖJO PAVADINIMAS");
            var c = csv.GetField("MOKĖJIMO PASKIRTIS");
            var d = csv.GetField("SUMA");
            var e = csv.GetField("DEBETAS/KREDITAS");
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
#nullable disable
    }
}
