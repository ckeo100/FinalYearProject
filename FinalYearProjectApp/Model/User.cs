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

namespace FinalYearProjectApp.Model
{
    public class User
    {
        public Guid UserUID { get; set; }
        public String FirstName { get; set; }
        public String SureName { get; set; }
        public String UserEmail { get; set; }
        public List<String> UserJobIDList { get; set; }
    }
    public class UserModel
    {
        public List<User> UserList = new List<User>();
        public Model.User user = new Model.User();

        public UserModel()
        {
            user.UserUID = Guid.NewGuid();
            user.FirstName = "Ciaran";
            user.SureName = "Keogh";
            user.UserEmail = "ciaranKeogh@Email.co.uk";
            user.UserJobIDList = new List<String>();//new List<Job>();

            UserList.Add(user);
        }

        public List<User> ShowAllUser()
        {
            //List<User> UserList = new List<User>();
            return UserList;
        }

        public List<Job> ShowUserJobList (string userID)
        {
            List<Job> userJobs = new List<Job>();
            return userJobs;
        }
    }
}