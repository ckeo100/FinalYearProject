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
using SQLite;
using SQLiteNetExtensions;

using FinalYearProjectApp.AppServices;
using Android.Util;
using SQLiteNetExtensions.Attributes;

namespace FinalYearProjectApp.Model
{
    public class User
    {
        
        [PrimaryKey, AutoIncrement, Column("_id")]
        public Guid UserUID { get; set; }
        [MaxLength(25)]
        public String FirstName { get; set; }
        [MaxLength(25)]
        public String SureName { get; set; }
        [MaxLength(25)]
        public String UserEmail { get; set; }
        [MaxLength(25)]
        public String Password { get; set; }
        
        
    }

    public class UserPotentialJob 
    {
        
        public Guid userUID { get; set; }
        [PrimaryKey]
        public string jobGuid { get; set; }
        public string jobName { get; set; }
        public string jobContactDetails { get; set; }
        //public object JobUID { get; internal set; }
    }
    public class UserModel
    {
        //public List<User> UserList = new List<User>();
        public User user = new Model.User();
        SqlDataHandler sqliteHandler = new SqlDataHandler();
         
        public List<User> showAllUser()
        {
            string path = sqliteHandler.getSqliteFolderLocation();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path, "user.db")))
                {
                    var userTable = connection.Table<User>();
                    List<User> userdata = userTable.ToList();
                    return userdata;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                List<User> emptyUser = new List<User>();
                return emptyUser;
            }
        }
        public User getCurrentUser()
        {
            string path = sqliteHandler.getSqliteFolderLocation();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path, "user.db")))
                {
                    var userTable = connection.Table<User>();
                    User userdata = userTable.FirstOrDefault();
                    return userdata;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                User emptyUser = new User();
                return emptyUser;
            }
        }
        public User showUserByID(Guid userID)
        {
            string path = sqliteHandler.getSqliteFolderLocation();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path, "user.db")))
                {
                    var userTable = connection.Table<User>();
                    var userdata = userTable.Where(x => x.UserUID == userID);
                    if(userdata != null)
                    {
                        User user = userdata.FirstOrDefault();
                        return user;
                    }
                    else
                    {
                        User emptyUser = new User();
                        return user;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                User emptyUser = new User();
                return emptyUser;
            }

        }
       
        public User showUserByCredentials(String userEmail, String userPassword)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
            var userTable = db.Table<User>();
            var userdata = userTable.Where(x => x.UserEmail == userEmail && x.Password == userPassword);
            if (userdata != null)
            {
                User user = userdata.FirstOrDefault();
                return user;
            }
            else
            {
                User emptyUser = new User();
                return user;
            }

        }

        internal List<UserPotentialJob> ShowUserJobAd(object userUID)
        {
            throw new NotImplementedException();
        }

        public List<UserPotentialJob> ShowUserJobAd (Guid userID)
        {
            List<UserPotentialJob> UserJobAd = new List<UserPotentialJob>();

            try
            {
                string path = sqliteHandler.getSqliteFolderLocation();
                //path += "/.config";
                var db = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd"));
                var UserJobAdTableData = db.Table<UserPotentialJob>();
                UserJobAd = UserJobAdTableData.Where(x => x.userUID == userID).ToList();
                return UserJobAd;
                
            }
            catch
            {
                return UserJobAd;
            }
            //List<Job> userJobs = new List<Job>();
            //return userJobs;
        }

        public void addToUserJobAd(Guid UserGuid, string jobID, string jobName, string contactDetails)
        {
            try
            {
                string path = sqliteHandler.getSqliteFolderLocation();
                //path += "/.config";
                var db = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd"));
                var UserJobAdTableData = db.Table<UserPotentialJob>();
                UserPotentialJob newPotentialJob = new UserPotentialJob();
                newPotentialJob.userUID = UserGuid;
                newPotentialJob.jobGuid = jobID;
                newPotentialJob.jobName = jobName;
                newPotentialJob.jobContactDetails = contactDetails;
                db.Insert(newPotentialJob);
                

            }
        
             catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                
            }
        }
        public void RemoveJobAdFromUserList(string jobID)
        {
            try
            {
                string path = sqliteHandler.getSqliteFolderLocation();
                //path += "/.config";
                var db = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd"));
                var UserJobAdTableData = db.Table<UserPotentialJob>();
                UserPotentialJob newPotentialJob = new UserPotentialJob();
                var rowCount = db.Delete<UserPotentialJob>(jobID);
              


            }

            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);

            }
        }
        public User getUserByCredentials(String userEmail, String userPassword)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
            var userTable = db.Table<User>();
            var userdata = userTable.Where(x => x.UserEmail == userEmail && x.Password == userPassword);
            if (userdata != null)
            {
                User user = userdata.FirstOrDefault();
                return user;
            }
            else
            {
                User emptyUser = new User();
                return user;
            }
        }

        public bool createNewUser(User user)
        {
            try
            {
                string path = sqliteHandler.getSqliteFolderLocation();
                var db = new SQLiteConnection(System.IO.Path.Combine( path, "user.db"));

                var userTableData = db.Table<User>();
                var userData = userTableData.Where(x => x.UserEmail == user.UserEmail).FirstOrDefault();

                if (userData == null)
                {
                    db.Insert(user);
                    return true;
                    //string message = "User Successfully Added";
                    //Toast.MakeText(this, message, ToastLength.Short).Show();
                }
                else
                {
                    return false;
                }


            }

            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                return false;
            }

        }
    }
}