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
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectApp
{
    
    [Activity(Label = "JobDetailsActivity")]
    public class JobDetailsActivity : Activity
    {
        JobModel jobModel = new JobModel();
        Job jobItem = new Job();
        public TextView txvJobLabel;
        public TextView txvEmploymentTypeText;
        public TextView txvSalaryText;
        public TextView txvRequiredQualificationsAndSkillsText;
        public TextView txvAddtionalQualificationsAndSkillsText;
        public TextView txvJobDescriptionText;
        public Guid jobGuid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.JobDetails);
            txvJobLabel = FindViewById<TextView>(Resource.Id.lblJobName);
            txvEmploymentTypeText = FindViewById<TextView>(Resource.Id.txvEmploymentType);
            txvSalaryText = FindViewById<TextView>(Resource.Id.txvSalary);
            txvRequiredQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvRequiredQualificationsAndSkills);
            txvAddtionalQualificationsAndSkillsText = FindViewById<TextView>(Resource.Id.txvAdditionalQualifictionAndSkills);
            txvJobDescriptionText = FindViewById<TextView>(Resource.Id.txvJobDescription);
            string jobGuidString = Intent.Extras.GetString("selectedJobGuid");
            jobGuid = Guid.Parse(jobGuidString);
            jobItem = jobModel.GetJob(jobGuid);

            txvJobLabel.Text = jobItem.JobName;
            txvEmploymentTypeText.Text += jobItem.JobEmploymentType;
            string completeSalary = string.Format("{0}-{1} per {2}", jobItem.JobSalaryMin, jobItem.JobSalaruMax, jobItem.JobSalaryRate);
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

            // Create your application here
        }
    }
}