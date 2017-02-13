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
using FinalYearProjectClassLibrary.Model;
using FinalYearProjectApp.AppServices;
//using MongoDB.Bson;
//using MongoDB.Driver;
using Java.Lang;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Json;

namespace FinalYearProjectApp.Model
{
    public class Id
    {

    }
    public class Job
    {
        public string JobID { get; set; }
        public string EmployeeID { get; set; }
        public string JobName { get; set; }
        public List<string> JobTags { get; set; }
        public string JobEmploymentType { get; set; }
        public List<string> JobBasicQualification { get; set; }
        public List<string> JobAdditionalSkillsAndQaulifications { get; set; }
        public List<string> JobPerferedSkillsAndQaulifications { get; set; }
        public string JobSalaryRate { get; set; }
        public int JobSalaryMin { get; set; }
        public int JobSalaryMax { get; set; }
        public string JobDescription { get; set; }
        public Address JobAddress { get; set; }
        public string RecruiterEmail { get; set; }

    }
    public class JobModel
    {
        private async Task<JsonValue> GetJsonAsync (string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    //.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    return jsonDoc;
                }
            }
        }


        MathService mathService = new MathService();
        public List<Job> ShowAllJobs()
        {
            //IMongoCollection<Job> jobColl = MongoConnection();
            //var jobQuery = from j in jobColl.AsQueryable<Job>()
            //               select j;//jobColl.Find(_ => true).ToList();//jobTempRespository.GetAllJobs();
            List<Job> jobList = new List<Job>();
            //foreach (Job j in jobQuery)
            //{
            //    jobList.Add(j);
            //}
            return jobList;
        }

        public Job GetJob(string JobID)
        {
            //IMongoCollection<Job> jobColl = MongoConnection();
            //var selectedJob = jobColl.Find(j => j.JobID == JobID).ToList().FirstOrDefault();
            Job selectedJob = new Job();
            return selectedJob;
        }
        public List<Job> GetJobsGeoLocation(double Latitude, double Longitude)
        {
            List<Job> Jobs = new List<Job>();
            return Jobs;
        }

        public List<Job> GetJobsFromUserList(List<string> userJobIdList)
        {
            //IMongoCollection<Job> jobColl = MongoConnection();
            List<Job> FullJobs = ShowAllJobs();
            List<Job> userJobList = new List<Job>();
            //foreach String Id in userJobIdList
            //Job jobItem = FullJobs.Find(j => string.Equals(j.JobID, Id)//jobColl.Find(j => string.Equals(j.JobID, Id)).FirstOrDefault();
            //UserJobList = jobColl.Find(j => )
            foreach (string Id in userJobIdList)
            {
                userJobList.Add(GetJob(Id));
            }
            return userJobList;
           
        
        }

        public List<Job> GetLocalJobAd(double currentLatitude, double currentLongitude)
        {
            //IMongoCollection<Job> jobColl = MongoConnection();
            //radius of the search criteria circle 
            var jsonList = GetJsonAsync(UrlBuilder.getJobApi()).Result;
            var jobList = JsonConvert.DeserializeObject<List<Job>>(jsonList.ToString());
            double searchDistanceInKM = 8;
            //radius of earth
            double radiusOfTheEarthInKm = 6371;
            double currentLatitudeRad = currentLatitude;
            double currentLongitudeRad = currentLongitude;
            double maxLat = currentLatitudeRad + mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double minLat = currentLatitudeRad - mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double maxLong = currentLongitudeRad + mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            double minLong = currentLongitudeRad - mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            List<Job> newJobsLIst = jobList;

            //List<Job> fullJobList = ShowAllJobs();

            //var searchCriteriaJobs = jobColl.Find(j => j.JobAddress.Latitiude >= minLat 
            //&& j.JobAddress.Latitiude <= maxLat
            //&& j.JobAddress.Longitude >= minLong
            //&& j.JobAddress.Longitude <= maxLong
            //).ToList();

            //return searchCriteriaJobs.ToList();
            List<Job> searchCriteriaJobs = jobList.Where(j => j.JobAddress.Latitiude >= minLat
           && j.JobAddress.Latitiude <= maxLat
           && j.JobAddress.Longitude >= minLong
           && j.JobAddress.Longitude <= maxLong).ToList();//.SelectMany(j => j.JobAddress.Latitiude >= minLat
           //&& j.JobAddress.Latitiude <= maxLat
           //&& j.JobAddress.Longitude >= minLong
           //&& j.JobAddress.Longitude <= maxLong).ToList();//retrievedJobList.Find(j => j.JobAddress.Latitiude >= minLat
            //&& j.JobAddress.Latitiude <= maxLat
            //&& j.JobAddress.Longitude >= minLong
            //&& j.JobAddress.Longitude <= maxLong)//.ToList();

            return searchCriteriaJobs;
        }
    }
}