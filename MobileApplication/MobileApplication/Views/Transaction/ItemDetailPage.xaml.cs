using System.ComponentModel;
using DbEntities.Entities;
using Xamarin.Forms;
using MobileApplication.ViewModels;

namespace MobileApplication.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Transaction transaction)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(transaction);
        }
    }
}