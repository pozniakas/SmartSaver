using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DbEntities.Models;
using MobileApplication.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    public partial class NewTransactionPage : ContentPage
    {
        public Transaction Transaction { get; set; }
        public NewTransactionPage()
        {
            InitializeComponent();
            BindingContext = new NewTransactionViewModel();
        }

        async void UploadFileButton_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick transaction receipt"
            });

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void UploadFilesButton_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick transaction receipts"
            });

            if (pickResult != null)
            {
                var imageList = new List<ImageSource>();

                foreach(var image in pickResult)
                {
                    var stream = await image.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));
                }
                collectionView.ItemsSource = imageList;
            }
        }
    }
}