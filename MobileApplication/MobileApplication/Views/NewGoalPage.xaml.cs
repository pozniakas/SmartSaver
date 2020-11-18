using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbEntities.Models;
using Xamarin.Forms;
using MobileApplication.ViewModels;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    public partial class NewGoalPage : ContentPage
    {
        public Goal Goal { get; set; }
        public NewGoalPage()
        {
            InitializeComponent();
            BindingContext = new NewGoalViewModel();
        }
    }
}