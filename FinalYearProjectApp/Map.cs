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
using Java.Lang;

namespace FinalYearProjectApp
{
    [Activity(Label = "Map")]
    public class Map : Activity, IOnMapReadyCallback, ILocationListener//, //Java.Lang.Object
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        private GoogleMap gMap;
        LatLng userPostion;
        double latitude;
        double longitude;
        public Location currentGPSLocation;
        LocationManager locationManager;
        string locationProvider;
        public JobAdModel jobAdModel = new JobAdModel();
        public JobModel jobModel = new JobModel();
        public List<Job> jobList = new List<Job>();
        List<JobAd> jobAds = new List<JobAd>();
        ImageButton btnSearchIcon;
        string SearchIcon;
        public string Url; 
        public Button btnMenu;
        public Button btnAdList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            ActionBar.Hide();
            //Create a new list of jobs
            jobAds = new List<JobAd>();
            //Initialise the location manager 
            InitializeLocationManager();
            // assign the current location using the last know location 
            currentGPSLocation = getLastKnownLocation();
            MathService mathService = new MathService();
            mathService.setBoundingCircle(currentGPSLocation.Latitude,currentGPSLocation.Longitude);
            UrlBuilder urlBuilder = new UrlBuilder();
            if (Intent.Extras != null)
            {
                SearchIcon = Intent.Extras.GetString("SearchIcon");
                Url = UrlBuilder.getLocalJobsWithSearch(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat, SearchIcon);
            }
            else
            {
                Url = UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat);
            }
            //string url = UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat);
            btnSearchIcon = FindViewById<ImageButton>(Resource.Id.btnSearchIcon);
            btnMenu = FindViewById<Button>(Resource.Id.btnMenu);
            btnAdList = FindViewById<Button>(Resource.Id.btnAdList);
            btnSearchIcon.Click += searchIcon_Click;
            btnMenu.Click += btnMenu_Click;
            btnAdList.Click += btnAdList_Click;
            new GetData(this).Execute(Url);//UrlBuilder.getLocalJobs(mathService.maxLong,mathService.minLong, mathService.maxLat,mathService.minLat));


        }

        private void btnAdList_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UserJobAdActivity));
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void searchIcon_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            EditText input = new EditText(this);
            
            alert.SetView(input);
            alert.SetTitle("Search For Jobs");
            alert.SetMessage("Look for jobs with keyword.");
            alert.SetPositiveButton("Search", (senderAlert, args) => {
                string searchCriteria = input.Text.ToString();
                if (!string.IsNullOrEmpty(searchCriteria))
                {
                    var intent = new Intent(this, typeof(Map));
                    intent.PutExtra("SearchIcon", input.Text);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Please enter your search critia!", ToastLength.Short).Show();
                }
                
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }
        //accquire the list of jobs and makes a http command call to them
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
                stream = http.GetHTTPData(urlString);
                return stream;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                activity.jobList = JsonConvert.DeserializeObject<List<Job>>(result);
                activity.SetUpMap();
                pd.Dismiss();

            }
        }

        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            //}


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
            gMap = googleMap;
            gMap.MyLocationEnabled = true;
           
            Criteria criteria = new Criteria();
            Location testCurrentLocation = getLastKnownLocation();

            if (testCurrentLocation != null)
            {
                currentGPSLocation = testCurrentLocation;
                latitude = currentGPSLocation.Latitude;
                longitude = currentGPSLocation.Longitude;
                userPostion = new LatLng(latitude, longitude);

                //MathService mathService = new MathService();
                //mathService.setBoundingCircle(currentGPSLocation.Latitude, currentGPSLocation.Longitude);
                //UrlBuilder urlBuilder = new UrlBuilder();

                //if (Intent.Extras != null)
                //{
                //    SearchIcon = Intent.Extras.GetString("SearchIcon");
                //    Url = UrlBuilder.getLocalJobsWithSearch(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat, SearchIcon);
                //}
                //else
                //{
                //    Url = UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat);
                //}

                //new GetData(this).Execute(Url);//UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat));

                if (jobList != null)
                {
                    foreach (Job job in jobList)
                    {
                        MarkerOptions jobMarkerOpt = new MarkerOptions();
                        jobMarkerOpt.SetPosition(new LatLng(job.JobAddress.Latitiude, job.JobAddress.Longitude));
                        jobMarkerOpt.SetTitle(job.JobName);
                        jobMarkerOpt.SetSnippet(job.JobDescription);
                        jobMarkerOpt.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.JobAdIconv3));
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
        // handles the event of the user clicking on a marker 
        public void onMakerClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            List<JobAd> jobAdList = jobAds;
            var item = e.Marker;
            IEnumerable<JobAd> selectedJobList =
                from JobAd in jobAdList
                where JobAd.jobMarkerID == item.Id
                select JobAd;
            List<JobAd> testList = selectedJobList.ToList();
            JobAd selectedJob = testList.FirstOrDefault();
            var intent = new Intent(this, typeof(JobAdDetails));
            intent.PutExtra("selectedJobGuid", selectedJob.jobAdGUID);
            StartActivity(intent);

        }

        //detects when there is a change in location and updates the system respective
        public void OnLocationChanged(Location location)
        {
            currentGPSLocation = location;
            Toast.MakeText(this, "Location has changed", ToastLength.Short).Show();
            if (currentGPSLocation == null)
            {
                //locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {

                gMap.MyLocationEnabled = true;
        
                Criteria criteria = new Criteria();
                Location testCurrentLocation = getLastKnownLocation();
                if (testCurrentLocation != null)
                {
                    currentGPSLocation = testCurrentLocation;
                    latitude = currentGPSLocation.Latitude;
                    longitude = currentGPSLocation.Longitude;
                    userPostion = new LatLng(latitude, longitude);

                    MathService mathService = new MathService();
                    mathService.setBoundingCircle(currentGPSLocation.Latitude, currentGPSLocation.Longitude);
                    UrlBuilder urlBuilder = new UrlBuilder();

                    if (Intent.Extras != null)
                    {
                        SearchIcon = Intent.Extras.GetString("SearchIcon");
                        Url = UrlBuilder.getLocalJobsWithSearch(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat, SearchIcon);
                    }
                    else
                    {
                        Url = UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat);
                    }

                    new GetData(this).Execute(Url);//UrlBuilder.getLocalJobs(mathService.maxLong, mathService.minLong, mathService.maxLat, mathService.minLat));

                    if (jobList != null)
                    {
                        foreach (Job job in jobList)
                        {
                            MarkerOptions jobMarkerOpt = new MarkerOptions();

                            jobMarkerOpt.SetPosition(new LatLng(job.JobAddress.Latitiude, job.JobAddress.Longitude));
                            jobMarkerOpt.SetTitle(job.JobName);
                            jobMarkerOpt.SetSnippet(job.JobDescription);
                            jobMarkerOpt.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.JobAdIconv3));
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
        // acquires the best possible location using the list of possible providers 
        private Location getLastKnownLocation()
        {
            List<string> providers = new List<string>(locationManager.GetProviders(true));
            Location currentBestLocation = null;
            foreach (string provider in providers)
            {
                Location currentlyTestedLocation = locationManager.GetLastKnownLocation(provider);
                if (currentlyTestedLocation == null)
                {
                    continue;
                }
                if (currentBestLocation == null || currentlyTestedLocation.Accuracy < currentlyTestedLocation.Accuracy)
                {
                    currentBestLocation = currentlyTestedLocation;
                }
            }
            return currentBestLocation;
        }


    }
}