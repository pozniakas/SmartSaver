using System.Threading.Tasks;
using ZXing.Mobile;
using Xamarin.Forms;
using MobileApplication.Services;

[assembly: Dependency(typeof(MobileApplication.Droid.QrScanningService))]

namespace MobileApplication.Droid
{
    public class QrScanningService : IQRScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Click mobile cancel button to cancel scanning.",
                CancelButtonText = "Cancel"
            };

            var scanResult = await scanner.Scan(optionsCustom);

            if (scanResult != null)
            {

                if (scanResult.Text != null)
                {
                    return scanResult.Text;
                }
            }
            return null;
        }
    }
}