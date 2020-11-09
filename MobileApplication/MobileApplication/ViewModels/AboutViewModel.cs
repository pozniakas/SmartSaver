using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApplication.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {


        public AboutViewModel()
        {
            Title = "Main";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}