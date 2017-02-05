using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;
using FinalYearProjectClassLibrary.Repository;
using FinalYearProjectClassLibrary.Services;

namespace FinalYearProjectClassLibrary.Model
{
    public class Job
    {
        public Guid JobUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public String JobName { get; set; }
        public List<String> JobTags { get; set; }
        public String JobEmploymentType { get; set; }
        public List<String> JobBasicQualification { get; set; }
        public List<String> JobAdditionalSkillsAndQaulifications { get; set; }
        public List<String> JobPerferedSkillsAndQaulifications { get; set; }
        public String JobSalaryRate { get; set; }
        public int JobSalaryMin { get; set; }
        public int JobSalaruMax { get; set; }
        public String JobDescription { get; set; }
        public Address JobAddress { get; set; }
        public string RecruiterEmail {get; set;}

    }
    public class JobModel
    {
        JobsTempRepository jobTempRespository = new JobsTempRepository();
        MathService mathService = new MathService();
        public List<Job> ShowAllJobs()
        {
            List<Job> JobList = jobTempRespository.GetAllJobs();
            return JobList;
        }

        public Job GetJob(Guid JobID)
        {
            Job selectedJob = jobTempRespository.GetJob(JobID);
            return selectedJob;
        }
        public List<Job> GetJobsGeoLocation(double Latitude, double Longitude)
        {
            List<Job> Jobs = new List<Job>();
            return Jobs;
        }

        public List<Job> GetLocalJobAd(double currentLatitude, double currentLongitude)
        {
            //radius of the search criteria circle 
            double searchDistanceInKM = 8;
            //radius of earth
            double radiusOfTheEarthInKm = 6371;
            double currentLatitudeRad = currentLatitude;
            double currentLongitudeRad = currentLongitude;
            double maxLat = currentLatitudeRad + mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double minLat = currentLatitudeRad - mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double maxLong = currentLongitudeRad + mathService.ConvertRadianToDegree(Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            double minLong = currentLongitudeRad - mathService.ConvertRadianToDegree(Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            List<Job> newJobsLIst = new List<Job>();

            List<Job> fullJobList = ShowAllJobs();

            IEnumerable<Job> searchCriteriaJobs =
                from Job in fullJobList
                where Job.JobAddress.Latitiude >= minLat && Job.JobAddress.Latitiude <= maxLat
                && Job.JobAddress.Longitude >= minLong && Job.JobAddress.Longitude <= maxLong
                select Job;


            return searchCriteriaJobs.ToList();
        }
    }
}
