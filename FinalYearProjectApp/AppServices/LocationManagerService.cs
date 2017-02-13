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
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace FinalYearProjectApp.AppServices
{
    public class LocationManagerService : ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        private GoogleMap gMap;
        LatLng userPostion;
        double latitude;
        double longitude;
        Location currentGPSLocation;
        LocationManager locationManager;
        string locationProvider;
        public string addressText;
        public string locationText;
        public Activity currentContext;

        public LocationManagerService(Activity context) 
        {
            this.currentContext = context;
            InitializeLocationManager();
        }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

       

        private void InitializeLocationManager()
        {
           // locationManager = (LocationManager)GetSystemService(LocationService);
            //throw new NotImplementedException();
        }

        public void OnLocationChanged(Location location)
        {
            throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

       
    }
}