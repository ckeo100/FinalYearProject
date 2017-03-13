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
using Android.Util;
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp.AppServices
{
    class SqlDataHandler
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public string getSqliteFolderLocation()
        {
            return folder;
        }

        public bool checkIfTableExsists()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "user.db")))
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

        public bool createDB()
        {
            try
            {
                using (var connnection = new SQLiteConnection(System.IO.Path.Combine(folder, "user.db")))
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

        
    }
}