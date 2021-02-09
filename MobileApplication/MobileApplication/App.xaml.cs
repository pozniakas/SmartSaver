using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace MobileApplication
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                //DependencyService.Register<MockDataStore>();
                Device.SetFlags(new[] { "Expander_Experimental" });
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //throw;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
