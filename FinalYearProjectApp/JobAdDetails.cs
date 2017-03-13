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
//using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.Model;
using SQLite;

namespace FinalYearProjectApp
{
    [Activity(Label = "JobAdDetails")]
    public class JobAdDetails : Activity
    {
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
        string jobString; //= Intent.Extras.GetString("selectedJobGuid");

        protected override async void OnCreate(Bundle savedInstanceState)
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
            jobItem = await jobModel.GetJob(jobString);

            txvJobLabel.Text = jobItem.JobName;
            txvEmploymentTypeText.Text += jobItem.JobEmploymentType;
            string completeSalary = string.Format("{0}-{1} per {2}", jobItem.JobSalaryMin, jobItem.JobSalaryMax, jobItem.JobSalaryRate);
            txvSalaryText.Text += completeSalary;
            foreach (string skill in jobItem.JobBasicQualification)
            {
                txvRequiredQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
            }
            foreach (string skill in jobItem.JobAdditionalSkillsAndQaulifications)
            {
                txvAddtionalQualificationsAndSkillsText.Text += string.Format(">{0}\n", skill);
            }

            txvJobDescriptionText.Text += jobItem.JobDescription;

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
                var user = userDataTable.FirstOrDefault();

                if (user != null)
                {
                    bool containsJobID = user.UserJobIDList.Any(idData => idData == jobString);

                    if (containsJobID ==  true)
                    {
                        Toast.MakeText(this, "You already have this job in your list", ToastLength.Short).Show();
                    }
                    else
                    {
                        user.UserJobIDList.Add(jobString);
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
    }
}