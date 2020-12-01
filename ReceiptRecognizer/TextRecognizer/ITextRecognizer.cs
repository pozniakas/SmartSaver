using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptRecognizer.TextRecognizer
{
    public interface ITextRecognizer
    {
        public Task<string> GetText(Bitmap image);
    }
}
