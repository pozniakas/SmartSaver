using System;
using System.Windows.Forms;
using SmartSaver.Controllers;
using SmartSaver.Desktop;

namespace SmartSaver
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            new ImageRecognizer().IsLoaded();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
