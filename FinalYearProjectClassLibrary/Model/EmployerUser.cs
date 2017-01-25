using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace FinalYearProjectClassLibrary.Model
{
    public class EmployerUser : User
    {
        public String RecruiterEmail { get; set; }
        public String PostedJobListJob { get; set; }
    }
}
