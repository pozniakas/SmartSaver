using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApplication.ViewModels;
using DbEntities.Entities;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage : ContentPage
    {
        public CategoryDetailPage(Category category)
        {
            InitializeComponent();
            BindingContext = new CategoryDetailViewModel(category);
        }
    }
}