using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReceiptRecognizer.TextRecognizer
{
    public interface ITextRecognizer
    {
        public string GetText(Bitmap image);
    }
}
