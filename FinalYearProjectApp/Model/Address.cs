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
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp.Model
{
    public class Address : GeoLocation
    {
        public String LocationLine1 { get; set; }
        public String LocationLIne2 { get; set; }
        public String LocationCity { get; set; }
        public String LocationPostCode { get; set; }

        
    }
}