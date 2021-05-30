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
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}