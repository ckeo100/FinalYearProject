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
using FinalYearProjectClassLibrary;
using FinalYearProjectClassLibrary.Repository;
using FinalYearProjectClassLibrary.Controllers;
using FinalYearProjectApp.Adaptors;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectApp
{
    [Activity(Label = "Job Details")]
    public class UserJobListActivity : Activity
    {
        private ListView userJobListView;
        private List<Job> userJobs;
        private FinalYearProjectClassLibrary.Controllers.UserController userManager;
        private JobsTempRepository jobTempRepository;
        private JobModel jobModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserJobListView);
            userJobListView = FindViewById<ListView>(Resource.Id.LTVUserJobs);
            userManager = new FinalYearProjectClassLibrary.Controllers.UserController();
            //jobTempRepository = new JobsTempRepository();
            userJobs = userManager.ShowUsersJobList();
            userJobListView.Adapter = new JobListAdaptor(this, userJobs);
            userJobListView.FastScrollEnabled = true;
            userJobListView.ItemClick += userJobListView_ItemClick;
        }

        private void userJobListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            var job = userJobs[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof(JobDetailsActivity));
            intent.PutExtra("selectedJobGuid", job.JobUID.ToString());

            StartActivityForResult(intent, 100);
           
        }
    }
}