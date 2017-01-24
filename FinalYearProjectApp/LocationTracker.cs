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

namespace FinalYearProjectApp
{
    [Activity(Label = "LocationTracker")]
    public class LocationTracker : Activity, ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        TextView addressText;
        Location currentGPSLocation;
        LocationManager locationManager;

        Button getAddress;

        string locationProvider;
        TextView locationText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CurrentLocation);
            addressText = FindViewById<TextView>(Resource.Id.txvAddressText);
            locationText = FindViewById<TextView>(Resource.Id.txvGPSLocation);
            getAddress = FindViewById<Button>(Resource.Id.btnGetAddress);

            // addressText = FindViewById<TextView>(Resource.Id.txvAddressText);
            // locationText = FindViewById<TextView>(Resource.Id.);
            FindViewById<TextView>(Resource.Id.btnGetAddress).Click += AddressButton_OnClick;

            InitializeLocationManager();
        }
        void InitializeLocationManager()
        {
            //The LocationManager class will listen for GPS updates from the device and notify the application by way of events. 
            //In this example we ask Android for the best location provider that matches a given set of Criteria 
            //and provide that provider to LocationManager.
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }

        public async void OnLocationChanged(Location location)
        {
            currentGPSLocation = location;
            if (currentGPSLocation == null)
            {
                locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                locationText.Text = string.Format("{0:f6},{1:f6}", currentGPSLocation.Latitude, currentGPSLocation.Longitude);
                Address address = await ReverseGeocodeCurrentLocation();
                DisplayAddress(address);
            }
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

        protected override void OnResume()
        {
            //Override OnResume so that Activity1 will begin listening to the LocationManager when the activity comes into the foreground:
            base.OnResume();
            locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
            //We reduce the demands on the battery by unsubscribing from the LocationManager when the activity goes into the background.
        }
        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            //Add an event handler called AddressButton_OnClick to Activity1.
            //This button allows the user to try and get the address from the latitude and longitude.
            //The snippet below contains the code for AddressButton_OnClick:
            if (currentGPSLocation == null)
            {
                addressText.Text = "Can't determine the current address. Try again in a few minutes.";
                return;
            }

            Address address = await ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(currentGPSLocation.Latitude, currentGPSLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                addressText.Text = deviceAddress.ToString();
            }
            else
            {
                addressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }
    }
}