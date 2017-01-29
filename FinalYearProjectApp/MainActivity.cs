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
    [Activity(Label = "MainMenu", Icon = "@drawable/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public Button btnUserLocation;
        public Button btnUserList;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            btnUserLocation = FindViewById<Button>(Resource.Id.btnCurrentLocation);
            btnUserList = FindViewById<Button>(Resource.Id.btnUserJobList);
            HandleEvents();
            // Set our view from the "main" layout resource

        }

        private void HandleEvents()
        {
            btnUserLocation.Click += btnUserLocation_Click;
            btnUserList.Click += btnUserList_Click;
            
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(UserJobListActivity));
            StartActivity(intent);
        }

        private void btnUserLocation_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LocationTracker));
            StartActivity(intent);
        }
    }
}

