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
using FinalYearProjectApp.AppServices;
using Newtonsoft.Json;

namespace FinalYearProjectApp
{
    [Activity()]
    public class UserJobListActivity : Activity
    {
        private ListView userJobListView;
        public List<UserPotentialJob> userJobs;
        private Button btnEmailToUser;
        //private UserJobListActivity activity;
        private FinalYearProjectClassLibrary.Controllers.UserController userManager;
        //private JobsTempRepository jobTempRepository;
        private JobModel jobModel;
        public UserModel userModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserJobListView);
            
            userJobListView = FindViewById<ListView>(Resource.Id.LTVUserJobs);
            btnEmailToUser = FindViewById<Button>(Resource.Id.btnSendListToEmail);
            userModel = new UserModel();
            userManager = new FinalYearProjectClassLibrary.Controllers.UserController();
            var currentUser = userModel.getCurrentUser();
            //jobTempRepository = new JobsTempRepository();
            //userModel.ShowUserJobList()
            //List<UserPotentialJob> cureentUserPotetnialJobList = new List<UserPotentialJob>();
            userJobs = userModel.ShowUserJobList(currentUser.UserUID);//new List<Job>();//userModel.user.UserJobList;//userManager.ShowUsersJobList();
            btnEmailToUser.Click += btnEmailToUser_Click;
            //if ( currentUser.UserUID != null)
            //{
            //    cureentUserPotetnialJobList = userModel.ShowUserJobList( currentUser.UserUID);
            //    foreach (UserPotentialJob potentialJob in cureentUserPotetnialJobList)
            //    {
            //        Job newJob = jobModel.GetJob(potentialJob.jobGuid).Result;
            //    }
            //}

            userJobListView.Adapter = new JobListAdaptor(this, userJobs);
            userJobListView.FastScrollEnabled = true;
            userJobListView.ItemClick += userJobListView_ItemClick;
            Window.SetTitle(userModel.user.UserEmail);
        }

        private void btnEmailToUser_Click(object sender, EventArgs e)
        {
            //UserModel usermodel = new UserModel();
            //User userDetails = usermodel.getCurrentUser();
            new GetData(this).Execute(UrlBuilder.getJobApi());

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("The current list of jobs that you have found. \n");


            //foreach(UserPotentialJob userJob in userJobs)
            //{
            //    sb.Append("\n-"+userJob.jobName+"");
            //    sb.Append("\n\t+" +userJob.+"");
            //}

            //Intent email = new Intent(Intent.ActionSend);
            //email.PutExtra(Intent.ExtraEmail, new string[] { userDetails.UserEmail });
            //email.PutExtra(Intent.ExtraSubject, "Test");
            //email.PutExtra(Intent.ExtraText, "Test");
            //email.SetType("message/rfc822");
            //StartActivity(Intent.CreateChooser(email, "Send Email"));

        }

        private void userJobListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            UserPotentialJob job = userJobs[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof(JobDetailsActivity));
            intent.PutExtra("selectedJobGuid", job.jobGuid.ToString());

            StartActivityForResult(intent, 100);
           
        }
        private class GetData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private UserJobListActivity activity;

            public GetData(UserJobListActivity activity)
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
                List<Job> jobList = JsonConvert.DeserializeObject<List<Job>>(result);
                UserModel usermodel = new UserModel();
                List<Job> newJobList = new List<Job>();
                User userDetails = usermodel.getCurrentUser();
                foreach(UserPotentialJob jobAd in activity.userJobs)
                {
                    newJobList = jobList.Where(x => x.JobUID == jobAd.jobGuid).ToList();
                }
                //List<Job> userJobList = jobList.Where(x => x.JobUID;//.Where<>;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("The current list of jobs that you have found. \n");


                foreach (Job JobDetails in newJobList)//UserPotentialJob userJob in userJobs)
                {
                    sb.AppendLine("\n\n-" + JobDetails.JobName + "");
                    sb.AppendLine("\n\t+Empolyment Type:" + JobDetails.JobEmploymentType + "");
                    sb.AppendLine("\n\t+Salary: " + JobDetails.JobSalaryMin + " to " + JobDetails.JobSalaryMax + " per " + JobDetails.JobSalaryRate + "");
                    sb.AppendLine("\n\t+Required Qaulifications:");
                    foreach(string qaulfication in JobDetails.JobBasicQualification)
                    {
                        sb.AppendLine("\n\t\t-"+qaulfication+"");
                    }
                    sb.AppendLine("\n\t+OptionalQaulifcations:");
                    foreach (string qaulfication in JobDetails.JobAdditionalSkillsAndQaulifications)
                    {
                        sb.AppendLine("\n\t\t-" + qaulfication + "");
                    }
                    foreach (string qaulfication in JobDetails.JobPerferedSkillsAndQaulifications)
                    {
                        sb.AppendLine("\n\t\t-" + qaulfication + "");
                    }
                    sb.AppendLine("\n\t+Description "+JobDetails.JobDescription+"");
                    sb.AppendLine("\n\t+Contact Details: "+JobDetails.RecruiterContactDetails+"");
                }

                Intent email = new Intent(Intent.ActionSend);
                email.PutExtra(Intent.ExtraEmail, new string[] { userDetails.UserEmail });
                email.PutExtra(Intent.ExtraSubject, "Your Lastest Job List");
                email.PutExtra(Intent.ExtraText, sb.ToString());
                email.SetType("message/rfc822");
                activity.StartActivity(Intent.CreateChooser(email, "Send Email"));


                //activity.SetUpMap();
                pd.Dismiss();
                //return list;


            }
        }
    }
}