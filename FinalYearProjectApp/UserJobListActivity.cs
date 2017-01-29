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
    [Activity(Label = "UserJobListActivity")]
    public class UserJobListActivity : Activity
    {
        private ListView userJobListView;
        private List<Job> userJobs;
        private FinalYearProjectClassLibrary.Controllers.UserController userManager;
        private JobsTempRepository jobTempRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserJobListView);
            userJobListView = FindViewById<ListView>(Resource.Id.LTVUserJobs);
            userManager = new FinalYearProjectClassLibrary.Controllers.UserController();
            jobTempRepository = new JobsTempRepository();
            userJobs = jobTempRepository.GetAllJobs();//userManager.ShowUsersJobList();
            userJobListView.Adapter = new JobListAdaptor(this, userJobs);
        }
    }
}