using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary
{
    public class Job
    {
        public Guid JobUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public String JobName { get; set; }
        public List<String> JobTags { get; set; }
        public String JobEmploymentType { get; set; }
        public List<String> JobBasicQualification { get; set; }
        public List<String> JobAdditionalSkillsAndQaulifications { get; set; }
        public List<String> JobPerferedSkillsAndQaulifications { get; set; }
        public String JobSalaryRate { get; set; }
        public int JobSalaryMin { get; set; }
        public int JobSalaruMax { get; set; }
        public String JobDescription { get; set; }
        public Address JobAddress { get; set; }

    }
}
