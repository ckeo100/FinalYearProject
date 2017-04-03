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
        LinearLayout linearLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.JobAdDetails);
            ActionBar.Hide();
            linearLayout = FindViewById<LinearLayout>(Resource.Id.MenuLinearLayout);
            linearLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#530053"));
            txvJobLabel = FindViewById<TextView>(Resource.Id.lblJobName);
            txvEmploymentTypeText = FindViewById<TextView>(Resource.Id.txvEmploymentType);
            txvSalaryText = FindViewById<TextView>(Resource.Id.txvSalary);
            txvRequiredQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvRequiredQualificationsAndSkills);
            txvRequiredQualificationsAndSkillsText.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            txvAddtionalQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvAdditionalQualifictionAndSkills);
            txvAddtionalQualificationsAndSkillsText.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            txvJobDescriptionText = FindViewById<TextView>(Resource.Id.txvJobDescription);
            txvJobDescriptionText.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            btnAddJobToList = FindViewById<Button>(Resource.Id.btnAddJobList);
            btnAddJobToList.SetBackgroundColor(Android.Graphics.Color.ParseColor("#B00035"));

            jobString = Intent.Extras.GetString("selectedJobGuid");
            new GetData(this).Execute(UrlBuilder.getJobSingle(jobString));

            btnAddJobToList.Click += addToListButton_Click;

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
                    List<UserPotentialJob> UserJobAd = userModel.ShowUserJobAd(user.UserUID);
                    bool containsJobID = UserJobAd.Any(x => x.jobGuid == jobString);//user.UserJobIDList.Any(idData => idData == jobString);

                    if (containsJobID == true)
                    {
                        Toast.MakeText(this, "You already have this job in your list", ToastLength.Short).Show();
                    }
                    else
                    {
                        userModel.addToUserJobAd(user.UserUID, jobString, jobItem.JobName, jobItem.RecruiterContactDetails);
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
                pd.Dismiss();
            }
            public void assignValue()
            {
                JobAdDetails activity = this.activity;
                activity.txvJobLabel.Text = activity.jobItem.JobName;
                activity.txvEmploymentTypeText.Text += activity.jobItem.JobEmploymentType;
                string completeSalary = string.Format("{0}-{1} per {2}", activity.jobItem.JobSalaryMin, activity.jobItem.JobSalaryMax, activity.jobItem.JobSalaryRate);
                activity.txvSalaryText.Text += completeSalary;
                string JobBasicQualificationString = "<body>";
                string JobAdditionalSkillsAndQaulificationsString = "<body>";
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
                int txtViewHightInPixels = activity.txvJobDescriptionText.LineCount * activity.txvJobDescriptionText.LineHeight;
                
                activity.txvJobDescriptionText.SetHeight(txtViewHightInPixels); 
                activity.txvJobDescriptionText.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();

            }
        }
    }
}