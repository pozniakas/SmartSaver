using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbEntities.Entities;
using MobileApplication.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalDetailPage : ContentPage
    {
        public GoalDetailPage(Goal goal)
        {
            InitializeComponent();
            BindingContext = new GoalDetailViewModel(goal);
        }
    }
}