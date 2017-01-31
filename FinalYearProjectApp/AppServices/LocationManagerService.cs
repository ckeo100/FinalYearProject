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

        //private void InitializeLocationManager()
        //{
        //    locationManager = (LocationManager)GetSystemService(LocationService);
        //    Criteria criteriaForLocationService = new Criteria
        //    {
        //        Accuracy = Accuracy.Fine
        //    };
        //    IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

        //    if (acceptableLocationProviders.Any())
        //    {
        //        locationProvider = acceptableLocationProviders.First();
        //    }
        //    else
        //    {
        //        locationProvider = string.Empty;
        //    }
        //    Log.Debug(TAG, "Using " + locationProvider + ".");
        //}

        //public async void OnLocationChanged(Location location)
        //{
        //    currentGPSLocation = location;
        //    if (currentGPSLocation == null)
        //    {
        //        locationText = "Unable to detemine your location.";
        //    }
        //    else
        //    {
        //        locationText = string.Format("{0:f6},{1:f6}", currentGPSLocation.Latitude, currentGPSLocation.Longitude);
        //        Address address = await ReverseGeocodeCurrentLocation();
        //    }
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);

        //}

        //async void GetCurrentLocation(object sender, EventArgs eventArgs)
        //{
        //    if(currentGPSLocation == null)
        //    {
        //        addressText = "Can't determine current address";
        //        return;
        //    }
        //    Address address = await ReverseGeocodeCurrentLocation();
        //    DisplayAddress(address);
        //}

        //async Task<Address> ReverseGeocodeCurrentLocation()
        //{
        //    Geocoder geocoder = new Geocoder(currentContext);
        //    IList<Address> addressList =
        //        await geocoder.GetFromLocationAsync(currentGPSLocation.Latitude, currentGPSLocation.Longitude, 10);
        //    Address address = addressList.FirstOrDefault();
        //    return address;
        //}

        //void DisplayAddress(Address address)
        //{
        //    if (address != null)
        //    {
        //        StringBuilder deviceAddress = new StringBuilder();
        //        for (int i = 0; i< address.MaxAddressLineIndex; i++)
        //        {
        //            deviceAddress.AppendLine(address.GetAddressLine(i));
        //        }
        //        addressText = deviceAddress.ToString();
        //    }
        //    else
        //    {
        //        addressText = "Unable to determine the address. Try again Later";
        //    }
        //}

        //public void OnProviderDisabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnProviderEnabled(string provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        //{
        //    throw new NotImplementedException();
        //}






    }
}