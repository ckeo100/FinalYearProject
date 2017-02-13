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
            
            SetUpMap();
            //GetData();
            //Common common = new Common();
            
            //Common.getJobApi());

        }



        //private class GetData :AsyncTask<string, Java.Lang.Void, string>
        //{
        //    private ProgressDialog pd = new ProgressDialog(Application.Context);
        //    private Map activity;

        //    public GetData(Map activity )
        //    {
        //        this.activity = activity;
        //    }

        //    protected override void OnPreExecute()
        //    {
        //        base.OnPreExecute();
        //        pd.Window.SetType(WindowManagerTypes.SystemAlert);
        //        pd.SetTitle("Please wait...");
        //        pd.Show();
        //    }
        //    protected override string RunInBackground(params string[] @params)
        //    {
        //        string stream = null;
        //        string urlString = @params[0];

        //        HttpDataHandler http = new HttpDataHandler();
        //        stream = http.GetHTTPData(urlString);
        //        return stream;
        //    }
        //    protected override void OnPostExecute(string result)
        //    {
        //        base.OnPostExecute(result);
                
        //        List<Job> list = JsonConvert.DeserializeObject<List<Job>>(result);

        //        pd.Dismiss();
        //        //return list;

                    
        //    }
        //}

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
            string provider = locationManager.GetBestProvider(criteria, false);
            if (locationManager.GetLastKnownLocation(provider) != null)
            {
                Location currentLocation = locationManager.GetLastKnownLocation(provider);
                latitude = currentLocation.Latitude;
                longitude = currentLocation.Longitude;
                userPostion = new LatLng(latitude, longitude);

                List<Job> jobList = new List<Job>();
                jobList = jobModel.GetLocalJobAd(latitude, longitude);

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
                        newJobAd.jobAdGUID = job.JobID;
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

            //if (gMap != null)
            //{

            //}


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
            Job selectedJob = jobModel.GetJob(selectedJobList.FirstOrDefault().jobAdGUID);
            var intent = new Intent(this, typeof(JobAdDetails));
            intent.PutExtra("selectedJobGuid", selectedJob.JobID.ToString());
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
                string provider = locationManager.GetBestProvider(criteria, false);
                if (locationManager.GetLastKnownLocation(provider) != null)
                {
                    Location currentLocation = locationManager.GetLastKnownLocation(provider);
                    latitude = currentLocation.Latitude;
                    longitude = currentLocation.Longitude;
                    userPostion = new LatLng(latitude, longitude);

                    List<Job> jobList = new List<Job>();
                    jobList = jobModel.GetLocalJobAd(latitude, longitude);

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
                            JobAd newJobAd = new JobAd
                            {
                                jobAdGUID = job.JobID,
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


    }
}