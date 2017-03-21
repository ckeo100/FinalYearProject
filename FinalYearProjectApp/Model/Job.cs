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
using System.Net.Http;






namespace FinalYearProjectApp.Model
{
    public class Id
    {
        [JsonProperty(PropertyName = "$oid")]
        public string oid { get; set; }
    }
    public class Job
    {
        //public Id _id { get; set; }
        public string JobUID { get; set; }
        public string EmployeeUID { get; set; }
        public string JobName { get; set; }
        public List<string> JobTags { get; set; }
        public string JobEmploymentType { get; set; }
        public List<string> JobBasicQualification { get; set; }
        public List<string> JobAdditionalSkillsAndQaulifications { get; set; }
        public List<string> JobPerferedSkillsAndQaulifications { get; set; }
        public string JobSalaryRate { get; set; }
        public double JobSalaryMin { get; set; }
        public double JobSalaryMax { get; set; }
        public string JobDescription { get; set; }
        public Address JobAddress { get; set; }
        public string RecruiterContactDetails { get; set; }

        //public Job()
        //{
        //    this.JobUID = Guid.Empty.ToString();
        //    this.EmployeeUID = Guid.Empty.ToString();
        //    this.JobName = "Job Name";
        //    new List<string> = new List<>(new int[] { 2, 3, 7 });
        //    this.JobTags = 
        //}

    }

    
        public class JobModel
    {
        public async Task<JsonValue> DownloadDataAsync( string url)
        {
            //Get data from webservice
            
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            //send of a request for data from mlab
            using (WebResponse response = await request.GetResponseAsync())
            {
                //get a stream of data
                using (Stream stream = response.GetResponseStream())
                {
                    //turn the stream data into jsondata
                    JsonValue resultantJson = await Task.Run(() => JsonObject.Load(stream));

                    return resultantJson;
                }
            }
           
        }

        MathService mathService = new MathService();
        public async Task<List<Job>> ShowAllJobs()
        {
            string builtUrl = UrlBuilder.getJobApi();
            JsonValue json = await DownloadDataAsync(builtUrl);
            string jsonString = json.ToString();
            var jobArray = JsonConvert.DeserializeObject<Job[]>(jsonString);
            List<Job> jobList = new List<Job>();

            return jobList = jobArray.ToList();
        }

        public async Task<Job> GetJob(string JobID)
        {
           
            string builtUrl = UrlBuilder.getJobSingle(JobID);
            HttpDataHandler http = new HttpDataHandler();
            string stream = http.GetHTTPData(builtUrl);
            //JsonValue json = await DownloadDataAsync(builtUrl);
            Job selectedJob = JsonConvert.DeserializeObject<List<Job>>(stream).FirstOrDefault();
           
            return selectedJob;
        }
        public List<Job> GetJobsGeoLocation(double Latitude, double Longitude)
        {
            List<Job> Jobs = new List<Job>();
            return Jobs;
        }

        public async Task<List<Job>> GetPotentialUserJobs(List<UserPotentialJob> potentialJobList)
        {
            List<Job> jobList = await ShowAllJobs();
            foreach( UserPotentialJob job in potentialJobList)
            {
                Job newJob = jobList.Where(j => j.JobUID == job.jobGuid).FirstOrDefault();
                jobList.Add(newJob);
            }
            return jobList;
            
        }

        //public List<Job> getJobsByList(List<UserPotentialJob> userPotentialJobList)
        //{
        //    List<Job> newJobList = new List<Job>();
        //    foreach potentialJobIn
        //}

        public async Task<List<Job>> GetLocalJobAd(List<Job> jobList,double currentLatitude, double currentLongitude)
        {
            //IMongoCollection<Job> jobColl = MongoConnection();
            //radius of the search criteria circle  
            string url = UrlBuilder.getJobApi();
            //JobModel.GetData getData = new GetData();
            //HttpDataHandler http = new HttpDataHandler();
            string stream = null;
            HttpDataHandler http = new HttpDataHandler();
            stream = http.GetHTTPData(url);
            //string stream = http.GetHTTPData(url);

            var jobArray = jobList.ToArray();//JsonConvert.DeserializeObject<List<Job>>(stream);

            double searchDistanceInKM = 1;
            //radius of earth
            double radiusOfTheEarthInKm = 6371;
            double currentLatitudeRad = currentLatitude;
            double currentLongitudeRad = currentLongitude;
            double maxLat = currentLatitudeRad + mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double minLat = currentLatitudeRad - mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double maxLong = currentLongitudeRad + mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            double minLong = currentLongitudeRad - mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));


            List<Job> fullJobList = jobArray.ToList();


            List<Job> searchCriteriaJobs = fullJobList.Where(j => j.JobAddress.Latitiude >= minLat
           && j.JobAddress.Latitiude <= maxLat
           && j.JobAddress.Longitude >= minLong
           && j.JobAddress.Longitude <= maxLong).ToList();
            return searchCriteriaJobs;
        }

        

        private object DownloadDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}