using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiptRecognizer.ObjectRecognizer
{
    public struct Coordinate<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
    }
}
