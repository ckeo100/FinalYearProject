using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary.Repository
{
    public class JobsTempRepository
    {
        new static List<Job> Jobs = new List<Job>()
        {
            new Job()
            {
                JobUID = Guid.Parse("9A521CC9-896E-4BFC-AB20-AEAACC227AD2"),
                EmployeeUID = Guid.Parse("917059E3-D0B3-427D-A4B1-F47668A098C8"),
                JobName = "Software Developer",
                JobTags = { "C#", "Software", "Developer"},
                JobEmploymentType = "Full-Time",
                JobBasicQualification = {"5 GCSE's C-A", "BSc Degree in a Relative field (COmputer Science, Programming, Software Engineer, Etc)"},
                JobAdditionalSkillsAndQaulifications = {"C#", "Sql"},
                JobPerferedSkillsAndQaulifications = {"Java", "PHP"},
                JobSalaryRate = "Annual",
                JobSalaryMin = 34000,
                JobSalaruMax = 50000,
                JobDescription = "Standard Software developer Job",
                JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = 52.4862, Latitiude = 1.8904},

            },
            new Job()
            {
                JobUID = Guid.Parse("9A521CC9-896E-4BFC-AB20-AEAACC227AD2"),
                EmployeeUID = Guid.Parse("917059E3-D0B3-427D-A4B1-F47668A098C8"),
                JobName = "Software Developer",
                JobTags = { "C#", "Software", "Developer"},
                JobEmploymentType = "Full-Time",
                JobBasicQualification = {"5 GCSE's C-A", "BSc Degree in a Relative field (COmputer Science, Programming, Software Engineer, Etc)"},
                JobAdditionalSkillsAndQaulifications = {"C#", "Sql"},
                JobPerferedSkillsAndQaulifications = {"Java", "PHP"},
                JobSalaryRate = "Annual",
                JobSalaryMin = 34000,
                JobSalaruMax = 50000,
                JobDescription = "Standard Software developer Job",
                JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = 52.4862, Latitiude = 1.8904},

            }
        };
    }
}
