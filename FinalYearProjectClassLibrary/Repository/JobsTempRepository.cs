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
        private List<Job> jobs = new List<Job>();

        
        public JobsTempRepository()
        {
            Job job1 = new Job();
            job1.JobUID = Guid.Parse("9A521CC9-896E-4BFC-AB20-AEAACC227AD2");
            job1.EmployeeUID = Guid.Parse("917059E3-D0B3-427D-A4B1-F47668A098C8");
            job1.JobName = "Software Developer";
            List<string> tagList = new List<string>();
            tagList.Add("C#");
            tagList.Add("Software");
            tagList.Add("Developer");
            job1.JobTags = tagList;
            job1.JobEmploymentType = "Full-Time";
            List<string> jobBasicQaulification = new List<string>();
            jobBasicQaulification.Add("BSc Degree in a Relative field(COmputer Science, Programming, Software Engineer, Etc)");
            job1.JobBasicQualification = jobBasicQaulification;
            List<string> additionalSkillsAndQaulifications = new List<string>();
            additionalSkillsAndQaulifications.Add("C#");
            job1.JobAdditionalSkillsAndQaulifications = additionalSkillsAndQaulifications;
            List<string> perferedSkills = new List<string>();
            job1.JobPerferedSkillsAndQaulifications = perferedSkills;
            job1.JobSalaryRate = "Annual";
            job1.JobSalaryMin = 34000;
            job1.JobSalaruMax = 50000;
            job1.JobDescription = "Standard Software developer Job";
            job1.JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = 52.4862, Latitiude = 1.8904 };
            //return jobTempRepository.GetAllJobs();
            jobs.Add(job1);

            Job job2 = new Job();
            job2.JobUID = Guid.Parse("9A521CC9-896E-4BFC-AB20-AEAACC227AD2");
            job2.EmployeeUID = Guid.Parse("917059E3-D0B3-427D-A4B1-F47668A098C8");
            job2.JobName = "Software Engineer";
            List<string> tagList2 = new List<string>();
            tagList2.Add("C#");
            tagList2.Add("Software");
            tagList2.Add("Engineer");
            job2.JobTags = tagList;
            job2.JobEmploymentType = "Full-Time";
            List<string> jobBasicQaulification2 = new List<string>();
            jobBasicQaulification.Add("BSc Degree in a Relative field(Computer Science, Programming, Software Engineer, Etc)");
            job2.JobBasicQualification = jobBasicQaulification2;
            List<string> additionalSkillsAndQaulifications2 = new List<string>();
            additionalSkillsAndQaulifications2.Add("C#");
            job2.JobAdditionalSkillsAndQaulifications = additionalSkillsAndQaulifications2;
            List<string> perferedSkills2 = new List<string>();
            job2.JobPerferedSkillsAndQaulifications = perferedSkills2;
            job2.JobSalaryRate = "Annual";
            job2.JobSalaryMin = 34000;
            job2.JobSalaruMax = 50000;
            job2.JobDescription = "Standard Software Engineer Job";
            job2.JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = 52.4862, Latitiude = 1.8904 };
            //return jobTempRepository.GetAllJobs();
            jobs.Add(job2);

        }

        public List<Job> GetAllJobs()
        {
            IEnumerable<Job> UserJobs =
                from Job in jobs
                select Job;

            return UserJobs.ToList<Job>();
        }

        public Job GetJob(Guid jobID)
        {
            IEnumerable<Job> UserJobs =
                from Job in jobs
                where Job.JobUID == jobID
                select Job;
            return UserJobs.FirstOrDefault();
        }
               
    }
}
