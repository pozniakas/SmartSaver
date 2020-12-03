using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbEntities.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApplication.ViewModels;


namespace MobileApplication.Views
{
    public partial class NewCategoryPage : ContentPage
    {
        public AboutPage _newAbout;

        public delegate void StatusUpdateHandlerCategory(object sender, ProgressEventArgs e);

        public static event StatusUpdateHandlerCategory OnUpdateStatusCategory;

        public Category Category { get; set; }
        public NewCategoryPage()
        {
            InitializeComponent();
            BindingContext = new NewCategoryViewModel();
            SaveButton.Clicked += SaveButton_Clicked;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            UpdateStatus("category");
        }
        private void UpdateStatus(string status)
        {
            var h = OnUpdateStatusCategory;
            if (h == null) return;

            ProgressEventArgs args = new ProgressEventArgs(status);
            OnUpdateStatusCategory(this, args);
        }
        public class ProgressEventArgs : EventArgs
        {
            public string Status { get; private set; }

            public ProgressEventArgs(string status)
            {
                Status = status;
            }
        }
    }
}