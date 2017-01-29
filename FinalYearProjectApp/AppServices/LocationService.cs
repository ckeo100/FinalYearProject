using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using Android.Runtime;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FinalYearProjectApp.AppServices
{
    public class LocationInfo
    {
        public TextView addressText { get; set; }
        public Location currentGPSLocation { get; set; }
        public LocationManager locationManager { get; set; }
    }
    public class LocationService
    {
        
        public LocationService() {

            }

        


    }
}