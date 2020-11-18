using DbEntities.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views.Transactions
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ObservableCollection<Transaction> Items { get; set; }

        public ListPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<Transaction>
            {
                new Transaction {Amount = 1, Details = "Detail1"},
                new Transaction {Amount = 2, Details = "Details asdf"}
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
