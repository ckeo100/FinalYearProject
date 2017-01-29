using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalYearProjectClassLibrary.Repository;
using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectClassLibrary.Controllers
{
    public class UserController
    {
        public JobModel jobModel = new JobModel();
        //private FinalYearProjectClassLibrary.Repository.JobsTempRepository jobTempRepository = new FinalYearProjectClassLibrary.Repository.JobsTempRepository();
        public UserController()
        {

        }
        public List<Job> ShowUsersJobList()
        {
            List<Job> jobList = jobModel.ShowAllJobs();
            return jobList ;
        }
    }
}
