using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplication.ViewModels
{
    class ScanQRCodeViewModel : BaseViewModel
    {

        public void OnAppearing()
        {
            IsBusy = true;
        }


    }
}
