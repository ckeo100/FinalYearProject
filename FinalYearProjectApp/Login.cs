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
using FinalYearProjectApp.AppServices;

namespace FinalYearProjectApp
{
    [Activity(Label = "Login", Icon = "@drawable/icon", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText edtEmailAddress;
        EditText edtPassword;
        Button btnLogin;
        Button btnRegister;
        UserModel usermodel = new UserModel();
        SqlDataHandler sqlhandler = new SqlDataHandler();

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
            checkedIfTableExsists();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var userData = usermodel.showUserByCredentials(edtEmailAddress.Text, edtPassword.Text);
                if (userData != null)
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

        private void checkedIfTableExsists()
        {
            bool doesExsists = sqlhandler.checkIfTableExsists();
            if (doesExsists == false)
            {
                sqlhandler.createDB();
            }
        }
    }
}