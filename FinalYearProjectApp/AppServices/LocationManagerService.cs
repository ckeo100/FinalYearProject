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

namespace FinalYearProjectApp.AppServices
{
    
        
    
    public class LocationManagerService //: ILocationListener
    {
        //public string addressText { get; set; }
        //public Location currentGPSLocation { get; set; }
        //public LocationManager locationManager { get; set; }
        //public string locationProvider { get; set; }
        //public string locationText { get; set; }
        //public Context currentContext { get; set; }

        //public LocationManagerService(Context context) {
        //    currentContext = context;

        //    InitializeLocationManager();
        //    }

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