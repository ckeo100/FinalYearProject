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
            //List<JobAd> JobAds = new List<JobAd>();

            //List<Job> jobList = jobModel.ShowAllJobs();


            //foreach (Job job in jobList)
            //{
            //    JobAd newJobAd = new JobAd();
            //    newJobAd.jobAdGUID = job.JobUID;
            //    newJobAd.jobTitle = job.JobName;
            //    newJobAd.jobDetails = job.JobDescription;
            //    newJobAd.latitude = job.JobAddress.Latitiude;
            //    newJobAd.longitude = job.JobAddress.Longitude;
            //    JobAds.Add(newJobAd);
            //}
                
            return jobAds;
        }
        //public List<JobAd> GetLocalJobAd(double currentLatitude, double currentLongitude)
        //{
        //    //radius of the search criteria circle 
        //    double searchDistanceInKM = 8;
        //    //radius of earth
        //    double radiusOfTheEarthInKm = 6371;
        //    double currentLatitudeRad = currentLatitude;
        //    double currentLongitudeRad = currentLongitude;
        //    double maxLat = currentLatitudeRad + mathService.ConvertRadianToDegree(searchDistanceInKM/radiusOfTheEarthInKm);
        //    double minLat = currentLatitudeRad - mathService.ConvertRadianToDegree(searchDistanceInKM/radiusOfTheEarthInKm);
        //    double maxLong = currentLongitudeRad + mathService.ConvertRadianToDegree(Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
        //    double minLong = currentLongitudeRad - mathService.ConvertRadianToDegree(Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
        //    List<JobAd> JobAds = new List<JobAd>();

        //    List<Job> jobList = jobModel.ShowAllJobs();

        //    IEnumerable<Job> searchCriteriaJobs =
        //        from Job in jobList
        //        where Job.JobAddress.Latitiude >= minLat && Job.JobAddress.Latitiude <= maxLat
        //        && Job.JobAddress.Longitude >= minLong && Job.JobAddress.Longitude <= maxLong
        //        select Job;
        //    foreach (Job job in searchCriteriaJobs)
        //    {
        //        JobAd newJobAd = new JobAd();
        //        newJobAd.jobAdGUID = job.JobUID;
        //        newJobAd.jobTitle = job.JobName;
        //        newJobAd.jobDetails = job.JobDescription;
        //        newJobAd.latitude = job.JobAddress.Latitiude;
        //        newJobAd.longitude = job.JobAddress.Longitude;
        //        JobAds.Add(newJobAd);

        //    }

        //    return JobAds;
        //}
    }
}
