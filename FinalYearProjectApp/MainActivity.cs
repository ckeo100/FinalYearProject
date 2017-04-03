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

namespace FinalYearProjectApp
{
    [Activity( Icon = "@drawable/JobAdIconv3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //public Button btnUserLocation;
        public Button btnUserList;
        public Button btnGoToMapView;
        LinearLayout linearLayout; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();
            //btnUserLocation = FindViewById<Button>(Resource.Id.btnCurrentLocation);
            linearLayout = FindViewById<LinearLayout>(Resource.Id.MenuLinearLayout);
            linearLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#530053"));
            btnUserList = FindViewById<Button>(Resource.Id.btnUserJobAd);
            btnUserList.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B00035"));
            btnGoToMapView = FindViewById<Button>(Resource.Id.btnToMapView);
            btnGoToMapView.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B00035"));
            HandleEvents();
            // Set our view from the "main" layout resource

        }

        private void HandleEvents()
        {
            //btnUserLocation.Click += btnUserLocation_Click;
            btnUserList.Click += btnUserList_Click;
            btnGoToMapView.Click += btnGoToMapView_Click;
            
        }

        private void btnGoToMapView_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Map));
            StartActivity(intent);
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(UserJobAdActivity));
            StartActivity(intent);
        }

        private void btnUserLocation_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LocationTracker));
            StartActivity(intent);
        }
    }
}

