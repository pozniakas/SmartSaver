using System.ComponentModel;
using Xamarin.Forms;
using MobileApplication.ViewModels;

namespace MobileApplication.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}