using System.Linq;
using Android.Util;
using FinalYearProjectApp.Model;

using SQLite;

namespace FinalYearProjectApp.AppServices
{
    class SqlDataHandler
    {
        public string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public string getSqliteFolderLocation()
        {
            //path// += "/.config";
            return path; //+= "/.config"; ;
        }

        public bool checkIfUserTableExists()
        {
            try
            {
                //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path, "user.db")))
                {
                    var userTable = connection.Table<User>();
                    var userdata = userTable.FirstOrDefault();
                    if (userdata != null)
                    {
                        return true;
                    }
                    else
                    {
                        
                        return false;
                    }
                    
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                return false;
            }

        }

        public bool checkIfJobTableExists()
        {
            try
            {
                //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd")))
                {
                    var JobListTable = connection.Table<UserPotentialJob>();
                    var JobListdata = JobListTable.FirstOrDefault();
                    if (JobListdata != null)
                    {
                        return true;
                    }
                    else
                    {

                        return false;
                    }

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                return false;
            }

        }

        public bool createUserDB()
        {
            try
            {
                //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                using (var connnection = new SQLiteConnection(System.IO.Path.Combine(path, "user.db")))
                {
                    connnection.CreateTable<User>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                return false;
            }

            
        }

        public bool createJobListDB()
        {
            try
            {
                //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                using (var connnection = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd")))
                {
                    connnection.CreateTable<UserPotentialJob>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("There is a SQLite Exception", ex.Message);
                return false;
            }


        }

        public void resetNewJobListDB()
        {
            var connnection = new SQLiteConnection(System.IO.Path.Combine(path, "UserJobAd"));
            connnection.DropTable<UserPotentialJob>();
            var connnection1 = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
            connnection1.DropTable<User>();
        }


    }
}