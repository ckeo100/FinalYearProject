using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary.Model
{
    public class Address : GeoLocation
    {
        public String LocationLine1 { get; set; }
        public String LocationLIne2 { get; set; }
        public String LocationCity { get; set; }
        public String LocationPostCode { get; set; }
        
    }
}
