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
            List<string> tagList1 = new List<string>();
            tagList1.Add("C#");
            tagList1.Add("Software");
            tagList1.Add("Developer");
            job1.JobTags = tagList1;
            job1.JobEmploymentType = "Full-Time";
            List<string> jobBasicQaulification1 = new List<string>();
            jobBasicQaulification1.Add("BSc Degree in a Relative field(Computer Science, Programming, Software Engineer, Etc)");
            job1.JobBasicQualification = jobBasicQaulification1;
            List<string> additionalSkillsAndQaulifications1 = new List<string>();
            additionalSkillsAndQaulifications1.Add("C#");
            additionalSkillsAndQaulifications1.Add("Html");
            additionalSkillsAndQaulifications1.Add("CSS");
            additionalSkillsAndQaulifications1.Add("SQL");
            additionalSkillsAndQaulifications1.Add("Python");
            additionalSkillsAndQaulifications1.Add("F#");
            additionalSkillsAndQaulifications1.Add("Java");
            additionalSkillsAndQaulifications1.Add("PHP");
            job1.JobAdditionalSkillsAndQaulifications = additionalSkillsAndQaulifications1;
            List<string> perferedSkills = new List<string>();
            job1.JobPerferedSkillsAndQaulifications = perferedSkills;
            job1.JobSalaryRate = "Annual";
            job1.JobSalaryMin = 34000;
            job1.JobSalaruMax = 50000;
            job1.JobDescription = "Standard Software developer Job";
            job1.JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = -1.88551, Latitiude = 52.482949 };
            job1.RecruiterEmail = "RecruiterEMail@gmail.com";
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
            job2.JobTags = tagList2;
            job2.JobEmploymentType = "Full-Time";
            List<string> jobBasicQaulification2 = new List<string>();
            jobBasicQaulification2.Add("BSc Degree in a Relative field(Computer Science, Programming, Software Engineer, Etc)");
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
            job2.JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = -1.8881910000000535, Latitiude = 52.483079 };
            //return jobTempRepository.GetAllJobs();
            job2.RecruiterEmail = "RecruiterEMail@gmail.com";
            jobs.Add(job2);

            Job job3 = new Job();
            job3.JobUID = Guid.Parse("3D4746E5-8393-4096-B905-2D9E804AB732");
            job3.EmployeeUID = Guid.Parse("033D06AF-CEBD-4067-AB57-78AECFD42858");
            job3.JobName = "OutOfBoundsJob";
            List<string> tagList3 = new List<string>();
            tagList3.Add("C#");
            tagList3.Add("Software");
            tagList3.Add("Engineer");
            job3.JobTags = tagList2;
            job3.JobEmploymentType = "Full-Time";
            List<string> jobBasicQaulification3 = new List<string>();
            jobBasicQaulification3.Add("BSc Degree in a Relative field(Computer Science, Programming, Software Engineer, Etc)");
            job3.JobBasicQualification = jobBasicQaulification3;
            List<string> additionalSkillsAndQaulifications3 = new List<string>();
            additionalSkillsAndQaulifications3.Add("C#");
            job3.JobAdditionalSkillsAndQaulifications = additionalSkillsAndQaulifications3;
            List<string> perferedSkills3 = new List<string>();
            job3.JobPerferedSkillsAndQaulifications = perferedSkills3;
            job3.JobSalaryRate = "Annual";
            job3.JobSalaryMin = 34000;
            job3.JobSalaruMax = 50000;
            job3.JobDescription = "Standard Software Engineer Job";
            job3.JobAddress = new Address { LocationLine1 = "Example Road 231", LocationLIne2 = "Binary Hill", LocationCity = "Samplevill", LocationPostCode = "faihafhpaf", Longitude = 0.1248, Latitiude = 51.4995 };
            //return jobTempRepository.GetAllJobs();
            job3.RecruiterEmail = "RecruiterEMail@gmail.com";
            jobs.Add(job3);

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
