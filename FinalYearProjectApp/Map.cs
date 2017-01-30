using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Util;

namespace FinalYearProjectApp
{
    [Activity(Label = "Map")]
    public class Map : Activity, IOnMapReadyCallback , ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        private GoogleMap gMap;
        LatLng userPostion;
        double latitude;
        double longitude;
        Location currentGPSLocation;
        LocationManager locationManager;
        string locationProvider;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            SetUpMap();

            InitializeLocationManager();
            // Create your application here
        }

        private void InitializeLocationManager()
        {
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
            Log.Debug(TAG, "Using" + locationProvider + ".");
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

        private void SetUpMap()
        {
            if (gMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);

            };
        }

        public void OnMapReady(GoogleMap googleMap)//, Location location)
        {
            gMap = googleMap;
            gMap.MyLocationEnabled = true;

            //latitude = location.Latitude;
            //longitude = location.Longitude

            userPostion = new LatLng(latitude, longitude);
            gMap.MoveCamera(CameraUpdateFactory.NewLatLng(userPostion));
            gMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));

        }

        public async void OnLocationChanged(Location location)
        {
            currentGPSLocation = location;
            if (currentGPSLocation == null)
            {
                //locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                //locationText.Text = string.Format("{0:f6},{1:f6}", currentGPSLocation.Latitude, currentGPSLocation.Longitude);
                //Address address = await ReverseGeocodeCurrentLocation();
                //DisplayAddress(address);
                latitude = currentGPSLocation.Latitude;
                longitude = currentGPSLocation.Longitude;

                userPostion = new LatLng(latitude, longitude);
                gMap.MoveCamera(CameraUpdateFactory.NewLatLng(userPostion));
                gMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));
            }
            
        }


    }
}