using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Recognizer.TextRecognizer
{
    public class ReceiptTextInterpreter
    {
        private string _receiptText;
        public ReceiptTextInterpreter(string receiptText)
        {
            _receiptText = receiptText;
        }

        public decimal Amount
        {
            get
            {
                var matchLine = Regex.Match(_receiptText, "^.*(moketi|mokėti|suma).*", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                if (!matchLine.Success)
                {
                    matchLine = Regex.Match(_receiptText, @"^.*(\d+)(\,|\.)\d{1,2}\s*eur.*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }

                var matchAmount = Regex.Match(matchLine.Value, @"((0(\.|,)\d\d)|([1-9]\d*((\.|,)\d\d)?))", RegexOptions.IgnoreCase);
                var amountStr = matchAmount.Value.Replace(',', '.');

                return decimal.TryParse(amountStr, out decimal parsedAmount) ? parsedAmount : 0;
            }
        }

        public DateTime DateTime
        {
            get
            {
                var dateMatch = Regex.Matches(_receiptText, @"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                var timeMatch = Regex.Matches(_receiptText, @"(\d|0\d|1\d|2[0-3]):[0-5]\d:[0-5]\d", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                var dateTimeStr = "";

                var possibleDate = dateMatch.FirstOrDefault(match => DateTime.TryParse(match.Value, out var date) && date < DateTime.UtcNow.AddDays(1));

                if (timeMatch.Count == 0)
                {
                    timeMatch = Regex.Matches(_receiptText, @"(\d|0\d|1\d|2[0-3]):[0-5]\d", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                }

                if (possibleDate != null && timeMatch.Count > 0)
                {
                    dateTimeStr = possibleDate.Value + " " + timeMatch.Last().Value;
                }
                else if (possibleDate != null)
                {
                    dateTimeStr = possibleDate.Value;
                }

                return DateTime.TryParse(dateTimeStr, out var date) ? date : DateTime.UtcNow;
            }
        }

        public string CounterParty
        {
            get
            {
                var counterParty = Regex.Match(_receiptText, @"^.*(uab|uab).*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                return counterParty.Value;
            }
        }

        public string Details
        {
            get
            {
                return DateTime.ToString("yyyy-MM-dd HH:mm:ss") + " | " + CounterParty;
            }
        }
    }
}
