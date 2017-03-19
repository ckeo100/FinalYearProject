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
//using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.Model;
using Android.Views;
using FinalYearProjectApp.AppServices;
using Newtonsoft.Json;

namespace FinalYearProjectApp
{
    
    [Activity(Label = "JobDetailsActivity")]
    public class JobDetailsActivity : Activity
    {
        JobModel jobModel = new JobModel();
        ContactHandler contactHandler = new ContactHandler();
        Job jobItem = new Job();
        bool isContactDetailEmail;
        public TextView txvJobLabel;
        public TextView txvEmploymentTypeText;
        public TextView txvSalaryText;
        public TextView txvRequiredQualificationsAndSkillsText;
        public TextView txvAddtionalQualificationsAndSkillsText;
        public TextView txvJobDescriptionText;
        public Button btnContactButton;
        public Guid jobGuid;
        string jobString;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.JobDetails);
            //new GetData(this).Execute(UrlBuilder.getJobApi());
            txvJobLabel = FindViewById<TextView>(Resource.Id.lblJobName);
            txvEmploymentTypeText = FindViewById<TextView>(Resource.Id.txvEmploymentType);
            txvSalaryText = FindViewById<TextView>(Resource.Id.txvSalary);
            txvRequiredQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvRequiredQualificationsAndSkills);
            txvAddtionalQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvAdditionalQualifictionAndSkills);
            txvJobDescriptionText = FindViewById<TextView>(Resource.Id.txvJobDescription);
            btnContactButton = FindViewById<Button>(Resource.Id.btnContectButton);
            btnContactButton.Click += btnContactButton_Click;
            jobString = Intent.Extras.GetString("selectedJobGuid");
            //jobItem = new Job();
            new GetData(this).Execute(UrlBuilder.getJobSingle(jobString));
            //jobGuid = Guid.Parse(jobGuidString);
            //jobItem = await jobModel.GetJob(jobString);

            


            // Create your application here
        }

        private void btnContactButton_Click(object sender, EventArgs e)
        {
            if (isContactDetailEmail == false)
            {
                var url = Android.Net.Uri.Parse(jobItem.RecruiterContactDetails);
                Intent intent = new Intent(Intent.ActionView, url);
                StartActivity(intent);
            }
            else
            {
                var intent = new Intent(this, typeof(EmailContact));
                intent.PutExtra("employeeEmail", jobItem.RecruiterContactDetails);
                StartActivity(intent);
            }
        }

        private class GetData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private JobDetailsActivity activity;
            //private JobDetailsActivity jobDetailsActivity;

            public GetData(JobDetailsActivity activity)
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
                activity.jobItem = JsonConvert.DeserializeObject<List<Job>>(result).FirstOrDefault();
                assignValue();
                //activity.SetUpMap();
                pd.Dismiss();
                //return list;


            }
            public void assignValue()
            {
                JobDetailsActivity activity = this.activity;
                activity.txvJobLabel.Text = activity.jobItem.JobName;
                activity.txvEmploymentTypeText.Text += activity.jobItem.JobEmploymentType;
                string completeSalary = string.Format("{0}-{1} per {2}", activity.jobItem.JobSalaryMin, activity.jobItem.JobSalaryMax, activity.jobItem.JobSalaryRate);
                activity.txvSalaryText.Text += completeSalary;
                foreach (string skill in activity.jobItem.JobBasicQualification)
                {
                    activity.txvRequiredQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
                }
                foreach (string skill in activity.jobItem.JobAdditionalSkillsAndQaulifications)
                {
                    activity.txvAddtionalQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
                }

                activity.txvJobDescriptionText.Text += activity.jobItem.JobDescription;
                activity.isContactDetailEmail = activity.contactHandler.IsContactEmailAddress(activity.jobItem.RecruiterContactDetails);
                if (activity.isContactDetailEmail == true)
                {
                    activity.btnContactButton.Text = "Contact Employer Via Email";
                }
                else
                {
                    activity.btnContactButton.Text = "Apply at website";
                }
            }
        }
    }
}