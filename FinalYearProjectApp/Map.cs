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
//using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.Model;
using FinalYearProjectApp.AppServices;
using Java.Lang;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace FinalYearProjectApp
{
    [Activity(Label = "Map")]
    public class Map : Activity, IOnMapReadyCallback, ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        private GoogleMap gMap;
        LatLng userPostion;
        double latitude;
        double longitude;
        Location currentGPSLocation;
        LocationManager locationManager;
        string locationProvider;
        public JobAdModel jobAdModel = new JobAdModel();
        public JobModel jobModel = new JobModel();
        public List<Job> jobList = new List<Job>();
        List<JobAd> jobAds = new List<JobAd>();
        //List<Job> retrievedJobs = new List<Job>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //new GetData(this).Execute(UrlBuilder.getJobApi());
            SetContentView(Resource.Layout.MapView);
            //jobAdModel = new JobAdModel();
            
            jobAds = new List<JobAd>();
            
            InitializeLocationManager();

            //get the current 
            new GetData(this).Execute(UrlBuilder.getJobApi());

        }

       


        private class GetData :AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private Map activity;

            public GetData(Map activity )
            {
                this.activity = activity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlString = @params[0];

                HttpDataHandler http = new HttpDataHandler();
                //stream = http.GetHTTPData(UrlBuilder.getJobApi());
                stream = http.GetHTTPData(urlString);
                return stream;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                activity.jobList = JsonConvert.DeserializeObject<List<Job>>(result);
                activity.SetUpMap();
                pd.Dismiss();
                //return list;


            }
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
            //string provider = locationManager.GetBestProvider(criteria, false);
            Location testCurrentLocation = getLastKnownLocation();
            //LocationManager locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);

            if (testCurrentLocation != null)
            {
                Location currentLocation = testCurrentLocation;
                latitude = currentLocation.Latitude;
                longitude = currentLocation.Longitude;
                userPostion = new LatLng(latitude, longitude);

                List<Job> newJobList = jobModel.GetLocalJobAd(jobList, latitude, longitude).Result;
                //new GetData(this).Execute(UrlBuilder.getJobApi());
                //List<Job> jobList = new List<Job>();
                //CurrentJobList = DownloadDataAsync().Result;//jobModel.GetLocalJobAd(latitude, longitude);

                if (jobList != null)
                {
                    foreach (Job job in jobList)
                    {
                        MarkerOptions jobMarkerOpt = new MarkerOptions();

                        jobMarkerOpt.SetPosition(new LatLng(job.JobAddress.Latitiude, job.JobAddress.Longitude));
                        //jobMarkerOpt.SetPosition(new LatLng(52.483079, -1.8861910000000535));
                        jobMarkerOpt.SetTitle(job.JobName);
                        jobMarkerOpt.SetSnippet(job.JobDescription);

                        jobMarkerOpt.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.JobAdIconv3));
                        //jobMarkerOpt.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
                        Marker marker = gMap.AddMarker(jobMarkerOpt);
                        JobAd newJobAd = new JobAd();
                        newJobAd.jobAdGUID = job.JobUID;
                        newJobAd.jobTitle = job.JobName;
                        newJobAd.jobDetails = job.JobDescription;
                        newJobAd.jobMarkerID = marker.Id;
                        newJobAd.latitude = job.JobAddress.Latitiude;
                        newJobAd.longitude = job.JobAddress.Longitude;

                        jobAds.Add(newJobAd);

                    }

                }
                

                gMap.InfoWindowClick += onMakerClick;

                gMap.MoveCamera(CameraUpdateFactory.NewLatLng(userPostion));
                gMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));
            }

        }

        public void onMakerClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            //throw new NotImplementedException();
            List<JobAd> jobAdList = jobAds;
            var item = e.Marker;
            IEnumerable<JobAd> selectedJobList =
                from JobAd in jobAdList
                where JobAd.jobMarkerID == item.Id
                select JobAd;
            List<JobAd> testList = selectedJobList.ToList();
            JobAd selectedJob = testList.FirstOrDefault();//await jobModel.GetJob(selectedJobList.FirstOrDefault().jobAdGUID);
            var intent = new Intent(this, typeof(JobAdDetails));
            //string jobGUID = "2DF131DF-0DA6-40D1-A861-72957BA184D6";
            intent.PutExtra("selectedJobGuid", selectedJob.jobAdGUID);
            StartActivity(intent);

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
                //Geocoder geocoder = new Geocoder(this);
                //gMap = googleMap;
                gMap.MyLocationEnabled = true;


                Criteria criteria = new Criteria();
                //LocationManager locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
                //string provider = locationManager.GetBestProvider(criteria, false);
                Location testCurrentLocation = getLastKnownLocation();
                if (testCurrentLocation != null)
                {
                    Location currentLocation = testCurrentLocation;
                    latitude = currentLocation.Latitude;
                    longitude = currentLocation.Longitude;
                    userPostion = new LatLng(latitude, longitude);

                    List<Job> newJobList = jobModel.GetLocalJobAd(jobList, latitude, longitude).Result;

                    if (newJobList != null)
                    {
                        foreach (Job job in newJobList)
                        {
                            MarkerOptions jobMarkerOpt = new MarkerOptions();

                            jobMarkerOpt.SetPosition(new LatLng(job.JobAddress.Latitiude, job.JobAddress.Longitude));
                            //jobMarkerOpt.SetPosition(new LatLng(52.483079, -1.8861910000000535));
                            jobMarkerOpt.SetTitle(job.JobName);
                            jobMarkerOpt.SetSnippet(job.JobDescription);

                            jobMarkerOpt.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.JobAdIconv3));
                            //jobMarkerOpt.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
                            Marker marker = gMap.AddMarker(jobMarkerOpt);
                            JobAd newJobAd = new JobAd
                            {
                                jobAdGUID = job.JobUID,
                                jobMarkerID = marker.Id,
                                jobTitle = job.JobName,
                                jobDetails = job.JobDescription,
                                latitude = job.JobAddress.Latitiude,
                                longitude = job.JobAddress.Latitiude,

                            };
                            jobAds.Add(newJobAd);



                        }
                    }

                    gMap.InfoWindowClick += onMakerClick;

                    gMap.MoveCamera(CameraUpdateFactory.NewLatLng(userPostion));
                    gMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));
                }

            }


        }

        private Location getLastKnownLocation()
        {
            //locationManager = Application.Context.
            List<string> providers = new List<string>(locationManager.GetProviders(true));// locationManager.GetProviders(true);
            Location currentBestLocation = null;
            foreach (string provider in providers)
            {
                Location currentlyTestedLocation = locationManager.GetLastKnownLocation(provider);
                if (currentlyTestedLocation == null)
                {
                    continue;
                }
                //if currentlyTestedLocation has better Accuracy then the current
                if (currentBestLocation == null || currentlyTestedLocation.Accuracy < currentlyTestedLocation.Accuracy)
                {
                    currentBestLocation = currentlyTestedLocation;
                }
            }
            return currentBestLocation;
        }


    }
}