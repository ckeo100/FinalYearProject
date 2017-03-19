using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalYearProjectClassLibrary.Model;
using Java.Net;
using Java.IO;

namespace FinalYearProjectApp.AppServices
{
    
    public class HttpDataHandler
    {
        static String stream = null;

        public HttpDataHandler() { }

        public string GetHTTPData(String urlString)
        {
            try
            {
                //stream = null;
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();

                if(urlConnection.ResponseCode == HttpStatus.Ok)
                {
                    BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while((line = r.ReadLine()) != null)
                        sb.Append(line);
                    stream = sb.ToString();
                    urlConnection.Disconnect();
                }

            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                System.Diagnostics.Debug.WriteLine(error);
            }
            return stream;
        }

        public void PostHTTPData(String urlString, String Json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();

                urlConnection.RequestMethod = "POST";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(Json);
                int length = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("char", "application/json"); ;

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch(Exception ex) { }

                var status = urlConnection.ResponseCode;
            }
            catch (Exception ex)
            { }
        }

        public void PutHTTPData(String urlString, String Json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();

                urlConnection.RequestMethod = "PUT";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(Json);
                int length = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("char", "application/json"); ;

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch (Exception ex)
                { }

                var status = urlConnection.ResponseCode;
            }
            catch (Exception ex)
            { }
        }

        public void DeleteHTTPData(String urlString, String Json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                
                urlConnection.RequestMethod = "DELETE";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(Json);
                int length = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("char", "application/json"); ;

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch (Exception ex)
                { }

                var status = urlConnection.ResponseCode;
            }
            catch (Exception ex)
            { }
        }
    }
}