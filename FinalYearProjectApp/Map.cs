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
using FinalYearProjectClassLibrary.Model;

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
        JobAdModel jobAdModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            jobAdModel = new JobAdModel();
            

            InitializeLocationManager();
            
            SetUpMap();
            
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

        public void OnMapReady(GoogleMap googleMap)
        {
            //Geocoder geocoder = new Geocoder(this);
            gMap = googleMap;
            gMap.MyLocationEnabled = true;


            Criteria criteria = new Criteria();
            //LocationManager locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
            String provider = locationManager.GetBestProvider(criteria, false);
            Location currentLocation = locationManager.GetLastKnownLocation(provider);
            latitude = currentLocation.Latitude;
            longitude = currentLocation.Longitude;
            userPostion = new LatLng(latitude, longitude);
            gMap.MoveCamera(CameraUpdateFactory.NewLatLng(userPostion));
            gMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));

            if (gMap != null)
            {
                List<JobAd> jobAdList = new List<JobAd>();
                jobAdList = jobAdModel.GetAllJobAds();

                foreach (JobAd jobAd in jobAdList)
                {
                    MarkerOptions jobMarkerOpt = new MarkerOptions();
                    jobMarkerOpt.SetPosition(new LatLng(jobAd.latitude, jobAd.longitude));
                    //jobMarkerOpt.SetPosition(new LatLng(52.483079, -1.8861910000000535));
                    jobMarkerOpt.SetTitle(jobAd.jobTitle);
                    gMap.AddMarker(jobMarkerOpt);
                }
            }
            

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