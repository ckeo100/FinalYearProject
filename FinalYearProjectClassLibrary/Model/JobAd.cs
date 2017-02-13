using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;
using FinalYearProjectClassLibrary.Services;


namespace FinalYearProjectClassLibrary.Model
{
    public class JobAd 
    {
        public Guid jobAdGUID { get; set; }
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
