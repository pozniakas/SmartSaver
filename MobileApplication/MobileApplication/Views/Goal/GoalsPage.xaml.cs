using Xamarin.Forms;
using MobileApplication.ViewModels;

namespace MobileApplication.Views
{
    public partial class GoalsPage : ContentPage
    {
        GoalsViewModel _viewModel;

        public GoalsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GoalsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}