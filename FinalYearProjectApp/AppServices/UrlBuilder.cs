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

        public static string getJobSingle(string id)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?={\"JobUID\":" + id + "}?apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

        public static string getJobApi()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COLLECTION_NAME}";
            StringBuilder strBuilder = new StringBuilder(baseUrl);
            strBuilder.Append("?apiKey=" + API_KEY);
            return strBuilder.ToString();
        }

    }
}