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
        public Category Category { get; set; }
        public NewCategoryPage()
        {
            InitializeComponent();
            BindingContext = new NewCategoryViewModel();
        }
    }
}