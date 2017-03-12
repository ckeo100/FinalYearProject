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
//using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp
{
    [Activity()]
    public class UserJobListActivity : Activity
    {
        private ListView userJobListView;
        private List<Job> userJobs;
        private FinalYearProjectClassLibrary.Controllers.UserController userManager;
        //private JobsTempRepository jobTempRepository;
        private JobModel jobModel;
        public UserModel userModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserJobListView);
            
            userJobListView = FindViewById<ListView>(Resource.Id.LTVUserJobs);
            userModel = new UserModel();
            userManager = new FinalYearProjectClassLibrary.Controllers.UserController();
            //jobTempRepository = new JobsTempRepository();
            List<Job> userJobs = new List<Job>();//userModel.user.UserJobList;//userManager.ShowUsersJobList();
            
            if ( userModel.user.UserJobIDList != null)
            {
                userJobs = userModel.ShowUserJobList( userModel.user.UserUID.ToString());
            }

            userJobListView.Adapter = new JobListAdaptor(this, userJobs);
            userJobListView.FastScrollEnabled = true;
            userJobListView.ItemClick += userJobListView_ItemClick;
            Window.SetTitle(userModel.user.UserEmail);
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