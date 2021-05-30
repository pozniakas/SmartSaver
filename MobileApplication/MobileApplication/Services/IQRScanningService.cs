using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication.Services
{
    public interface IQRScanningService
    {
        Task<string> ScanAsync();
    }
}
