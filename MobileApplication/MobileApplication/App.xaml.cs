using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApplication.Services;
using MobileApplication.Views;

namespace MobileApplication
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            Device.SetFlags(new[] { "Expander_Experimental" });
            MainPage = new AppShell();
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
