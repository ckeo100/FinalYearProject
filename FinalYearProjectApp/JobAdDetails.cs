using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
//using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.Model;
using SQLite;
using System.Collections.Generic;
using FinalYearProjectApp.AppServices;
using Newtonsoft.Json;
using Android.Text;

namespace FinalYearProjectApp
{
    [Activity()]
    public class JobAdDetails : Activity
    {
        UserModel userModel = new UserModel();
        JobModel jobModel = new JobModel();
        Job jobItem = new Job();
        public TextView txvJobLabel;
        public TextView txvEmploymentTypeText;
        public TextView txvSalaryText;
        public TextView txvRequiredQualificationsAndSkillsText;
        public TextView txvAddtionalQualificationsAndSkillsText;
        public TextView txvJobDescriptionText;
        public Button btnAddJobToList;
        public Guid jobGuid;
        string jobString;//= Intent.Extras.GetString("selectedJobGuid");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.JobAdDetails);
            
            txvJobLabel = FindViewById<TextView>(Resource.Id.lblJobName);
            txvEmploymentTypeText = FindViewById<TextView>(Resource.Id.txvEmploymentType);
            txvSalaryText = FindViewById<TextView>(Resource.Id.txvSalary);
            txvRequiredQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvRequiredQualificationsAndSkills);
            txvAddtionalQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvAdditionalQualifictionAndSkills);
            txvJobDescriptionText = FindViewById<TextView>(Resource.Id.txvJobDescription);
            btnAddJobToList = FindViewById<Button>(Resource.Id.btnAddJobList);

            jobString = Intent.Extras.GetString("selectedJobGuid");
            new GetData(this).Execute(UrlBuilder.getJobSingle(jobString));
            //jobItem = await jobModel.GetJob(jobString);

            //txvJobLabel.Text = jobItem.JobName;
            //txvEmploymentTypeText.Text += jobItem.JobEmploymentType;
            //string completeSalary = string.Format("{0}-{1} per {2}", jobItem.JobSalaryMin, jobItem.JobSalaryMax, jobItem.JobSalaryRate);
            //txvSalaryText.Text += completeSalary;
            //foreach (string skill in jobItem.JobBasicQualification)
            //{
            //    txvRequiredQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
            //}
            //foreach (string skill in jobItem.JobAdditionalSkillsAndQaulifications)
            //{
            //    txvAddtionalQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
            //}

            //txvJobDescriptionText.Text += jobItem.JobDescription;

            btnAddJobToList.Click += addToListButton_Click;


            // Create your application here
        }



        private void addToListButton_Click(object sender, EventArgs e)
        {
            try
            {
                String path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
                var userDataTable = db.Table<User>();
                var user = userModel.getCurrentUser();//userDataTable.FirstOrDefault();

                if (user != null)
                {
                    List<UserPotentialJob> userJobList = userModel.ShowUserJobList(user.UserUID);
                    bool containsJobID = userJobList.Any(x => x.jobGuid == jobString);//user.UserJobIDList.Any(idData => idData == jobString);

                    if (containsJobID == true)
                    {
                        Toast.MakeText(this, "You already have this job in your list", ToastLength.Short).Show();
                    }
                    else
                    {
                        userModel.addToUserJobList(user.UserUID, jobString, jobItem.JobName, jobItem.RecruiterContactDetails);
                        StartActivity(typeof(Map));
                    }

                }
                else
                {
                    Toast.MakeText(this, "There is a issues wil you user details", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }


        }
        private class GetData : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private JobAdDetails activity;
            //private JobDetailsActivity jobDetailsActivity;

            public GetData(JobAdDetails activity)
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
                JobAdDetails activity = this.activity;
                activity.txvJobLabel.Text = activity.jobItem.JobName;
                activity.txvEmploymentTypeText.Text += activity.jobItem.JobEmploymentType;
                string completeSalary = string.Format("{0}-{1} per {2}", activity.jobItem.JobSalaryMin, activity.jobItem.JobSalaryMax, activity.jobItem.JobSalaryRate);
                activity.txvSalaryText.Text += completeSalary;
                string JobBasicQualificationString = "<body> <br /> ";
                string JobAdditionalSkillsAndQaulificationsString = "<body> <br />";
                foreach (string skill in activity.jobItem.JobBasicQualification)
                {

                    JobBasicQualificationString += "<br /> &#8226;" + skill + "";
                    //activity.txvRequiredQualificationsAndSkillsText.SetText(JobBasicQualificationString.ToCharArray(), 0, JobBasicQualificationString.ToCharArray().Length);// += string.Format(@"\u2022 {0}\n", skill);
                }
                JobBasicQualificationString += "</body>";
                activity.txvRequiredQualificationsAndSkillsText.TextFormatted = Html.FromHtml(JobBasicQualificationString); //.SetText(JobBasicQualificationString.ToCharArray(), 0, JobBasicQualificationString.ToCharArray().Length);
                foreach (string skill in activity.jobItem.JobAdditionalSkillsAndQaulifications)
                {
                    JobAdditionalSkillsAndQaulificationsString += "<br /> &#8226;" + skill + "";
                    //activity.txvAddtionalQualificationsAndSkillsText.Text += string.Format(@"\u2022 {0}\n", skill);
                }
                JobAdditionalSkillsAndQaulificationsString += "</body>";
                activity.txvAddtionalQualificationsAndSkillsText.TextFormatted = Html.FromHtml(JobAdditionalSkillsAndQaulificationsString);//SetText( JobAdditionalSkillsAndQaulificationsString.ToCharArray(), 0, JobAdditionalSkillsAndQaulificationsString.ToCharArray().Length);
                //Android.Text.FromHtmlOptions.
                activity.txvJobDescriptionText.Text = activity.jobItem.JobDescription;

            }
        }
    }
}