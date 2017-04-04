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

namespace FinalYearProjectApp.AppServices
{
    public class UrlBuilder
    {
        private static String DB_NAME = "finalyearprojectdb";
        private static String COLLECTION_NAME = "Jobs";
        private static String API_KEY = "cCSf591FvBL9vhcrMqyvaLRQkT-je-vQ";
        private static String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";

        public static string getJobSingle(string id)
        {
            //String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?q={\"JobUID\":\"" + id + "\"}&apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

        public static string getJobApi()
        {
            //String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

        public static string getLocalJobs(double maxLongitude, double minLongitude, double maxLatitiude, double minLatitiude)
        {
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?q={$and: [ { \"JobAddress.Longitude\":  {$gte: "+ minLongitude+", $lt: "+ maxLongitude +"}  }, { \"JobAddress.Latitiude\": {$gte: "+minLatitiude+", $lt: "+maxLatitiude+"}} ]}&apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

        public static string getLocalJobsWithSearch(double maxLongitude, double minLongitude, double maxLatitiude, double minLatitiude, string searchString)
        {
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            //https://api.mlab.com/api/1/databases/finalyearprojectdb/collections/Jobs?q={$and: [ { "JobAddress.Longitude":  {$gte: -1.89997322985873, $lt: -1.87043883014127}  }, { "JobAddress.Latitiude": {$gte: 52.4738174139408, $lt: 52.4918038460592}}, { $or : [ {"JobName": {"$regex": "software", $options: 'i'}}, {"JobTags": {"$regex": "software", $options: 'i'}} ] } ]}&apiKey=cCSf591FvBL9vhcrMqyvaLRQkT-je-vQ
            strBuilder.Append("?q={$and: [ { \"JobAddress.Longitude\":  {$gte: " + minLongitude + ", $lt: " + maxLongitude + "}  }, { \"JobAddress.Latitiude\": {$gte: " + minLatitiude + ", $lt: " + maxLatitiude + "}}, { $or: [{\"JobName\":{\"$regex\": \""+ searchString+ "\", $options: 'i'}},{\"JobTags\":{\"$regex\": \"" + searchString + "\", $options: 'i'}}]} ]}&apiKey=" + API_KEY);
            return strBuilder.ToString();
        }


    }
}