namespace ReceiptRecognizer
{
    public static class Statistics
    {
        private static int imagesRecognized = 0;

        public static void AddRecognizingImage()
        {
            lock ((object) imagesRecognized)
            {
                imagesRecognized++;
            }
        }
    }
}
