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

    }
    public class Job
    {
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
        public string RecruiterEmail { get; set; }

    }
    public class JobModel
    {
        public async Task<List<Job>> DownloadDataAsync()
        {
            string url = UrlBuilder.getJobApi();

            var httpClient = new HttpClient();
            var downloadTask = httpClient.GetStringAsync(url);//.ConfigureAwait(continueOnCapturedContext: false);
            string content = await downloadTask;
            List<Job> jobList= new List<Job>();
            jobList = JsonConvert.DeserializeObject<List<Job>>(content);
            return jobList;
            //JavaScriptSerializer serializer = new JavaScriptSerializer();


            //var result = JsonConvert.DeserializeObject<RootObject>(content);
            //JsonValue jsonValue = JsonValue.Parse(content);
            //JsonObject jsonObject = jsonValue as JsonObject;
            //IList<JsonToken> tokenList = jsonObject.Select(JsonConvert.DeserializeObject<T>).ToList();
            //return content;
            //Console.Out.WriteLine("Response: \r\n {0}", content);
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
            string url = UrlBuilder.getJobApi();
            //JobModel.GetData getData = new GetData();
            //HttpDataHandler http = new HttpDataHandler();

            //string stream = http.GetHTTPData(url);
            List<Job> list = DownloadDataAsync().Result;//JsonConvert.DeserializeObject<List<Job>>(stream);
            //var jsonList = getData.jobList;//GetData(); //GetJsonAsync(UrlBuilder.getJobApi()).Result;
            //var jobList = JsonConvert.DeserializeObject<List<Job>>(jsonList.ToString());
            double searchDistanceInKM = 8;
            //radius of earth
            double radiusOfTheEarthInKm = 6371;
            double currentLatitudeRad = currentLatitude;
            double currentLongitudeRad = currentLongitude;
            double maxLat = currentLatitudeRad + mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double minLat = currentLatitudeRad - mathService.ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            double maxLong = currentLongitudeRad + mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            double minLong = currentLongitudeRad - mathService.ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(mathService.ConvertDegreesToRadians(currentLatitude));
            

            //List<Job> fullJobList = ShowAllJobs();

            //var searchCriteriaJobs = jobColl.Find(j => j.JobAddress.Latitiude >= minLat 
            //&& j.JobAddress.Latitiude <= maxLat
            //&& j.JobAddress.Longitude >= minLong
            //&& j.JobAddress.Longitude <= maxLong
            //).ToList();

            //return searchCriteriaJobs.ToList();
            List<Job> searchCriteriaJobs = list.Where(j => j.JobAddress.Latitiude >= minLat
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