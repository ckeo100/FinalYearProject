using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary.Model
{
    public class JobAd 
    {
        public Guid jobAdGUID { get; set; }
        public String jobTitle { get; set; }
        public String jobDetails { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    
    public class JobAdModel
    {
        JobModel jobModel = new JobModel();

        public List<JobAd> GetAllJobAds()
        {
            List<JobAd> JobAds = new List<JobAd>();

            List<Job> jobList = jobModel.ShowAllJobs();


            foreach (Job job in jobList)
            {
                JobAd newJobAd = new JobAd();
                newJobAd.jobAdGUID = job.JobUID;
                newJobAd.jobTitle = job.JobName;
                newJobAd.jobDetails = job.JobDescription;
                newJobAd.latitude = job.JobAddress.Latitiude;
                newJobAd.longitude = job.JobAddress.Longitude;
                JobAds.Add(newJobAd);
            }
                


            return JobAds;
        }
    }
}
