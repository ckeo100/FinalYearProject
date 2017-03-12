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
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp
{
    [Activity(Label = "Login", Icon = "@drawable/icon", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText edtEmailAddress;
        EditText edtPassword;
        Button btnLogin;
        Button btnRegister;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);
            edtEmailAddress = FindViewById<EditText>(Resource.Id.etxLoginUserEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.etxLoginPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                String path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
                var data = db.Table<User>();
                var data1 = data.Where(x => x.UserEmail == edtEmailAddress.Text && x.Password == edtPassword.Text).FirstOrDefault();

                if (data1 != null)
                {
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    Toast.MakeText(this, "User Email or password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        private string CreateDB()
        {
            var output = "";
            output += "Creating Database if it dosen't exsist";
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
            output += "\b Database Created...";
            return output;

        }
    }
}