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
using FinalYearProjectApp.AppServices;
using Android.Util;

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
        public List<String> UserJobIDList { get; set; }
    }
    public class UserModel
    {
        public List<User> UserList = new List<User>();
        public Model.User user = new Model.User();
        SqlDataHandler sqliteHandler = new SqlDataHandler();
        

        public UserModel()
        {
            user.UserUID = Guid.NewGuid();
            user.FirstName = "Ciaran";
            user.SureName = "Keogh";
            user.UserEmail = "ciaranKeogh@Email.co.uk";
            user.Password = "Password";
            user.UserJobIDList = new List<String>();//new List<Job>();

            UserList.Add(user);
        }

        public User showUserByID(Guid userID)
        {
            string path = sqliteHandler.getSqliteFolderLocation();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine()))
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
            string path = sqliteHandler.getSqliteFolderLocation();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine()))
                {
                    var userTable = connection.Table<User>();
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
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                User emptyUser = new User();
                return emptyUser;
            }

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