using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebAPI.Models;

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
                    //_readRecord = LuminorEnReadRecord;
                    break;
                // Luminor LT
                case "Data,Laikas,Suma":
                    break;
                // Seb EN
                case "INSTRUCTION ID,DATE,CURRENCY":
                    _readRecord = new ReadRecord(SebEnReadRecord);
                    break;
                // Seb LT
                case "DOK NR.,DATA,VALIUTA":
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
    }
}
