using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalYearProjectClassLibrary.Model
{
    public class JobSeekerUser : User
    {
        public Guid JobSeekerUID { get; set; }
        public List<Job> PotentialJobList { get; set; }
    }
}
