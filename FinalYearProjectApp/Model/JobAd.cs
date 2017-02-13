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
using FinalYearProjectApp.Model;
using FinalYearProjectApp.AppServices;

namespace FinalYearProjectApp.Model
{
    public class JobAd
    {
        public String jobAdGUID { get; set; }
        public String jobMarkerID { get; set; }
        public String jobTitle { get; set; }
        public String jobDetails { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class JobAdModel
    {
        JobModel jobModel = new JobModel();
        MathService mathService = new MathService();
        public List<JobAd> jobAds { get; set; }

        public JobAd getJobAdBy(Guid jobAdId)
        {
            JobAd jobAd = GetAllJobAds().FirstOrDefault();

            return jobAd;
        }

        public List<JobAd> GetAllJobAds()
        {

            return jobAds;
        }

    }
}