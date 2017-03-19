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
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        EditText edtFirstName;
        EditText edtLastName;
        EditText edtEmail;
        EditText edtConfirmEmail;
        EditText edtPassword;
        EditText edtConfirmPassword;
        Button btnRegisterAccount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            edtFirstName = FindViewById<EditText>(Resource.Id.etxResgisterFirstName);
            edtLastName = FindViewById<EditText>(Resource.Id.etxRegisterLastName);
            edtEmail = FindViewById<EditText>(Resource.Id.etxRegisterEmailAddress);
            edtConfirmEmail = FindViewById<EditText>(Resource.Id.etxRegisterConfirmEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.etxRegisterPassword);
            edtConfirmPassword = FindViewById<EditText>(Resource.Id.etxRegisterConfirmPassword);
            btnRegisterAccount = FindViewById<Button>(Resource.Id.btnRegisterAccount);
            edtPassword.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;
            edtConfirmPassword.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;
            btnRegisterAccount.Click += btnRegisterAccount_Click;
            
        }

        private void btnRegisterAccount_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string firstname = edtFirstName.Text;
            string lastName = edtLastName.Text;
            string email = edtEmail.Text;
            string confirmEmail = edtConfirmEmail.Text;
            string password = edtPassword.Text;
            string confirmPassword = edtConfirmPassword.Text;
            if(string.IsNullOrEmpty(edtFirstName.Text)|| string.IsNullOrEmpty(edtLastName.Text) || string.IsNullOrEmpty(edtPassword.Text) || string.IsNullOrEmpty(edtEmail.Text) || string.IsNullOrEmpty(edtConfirmEmail.Text) || string.IsNullOrEmpty(edtConfirmPassword.Text))
            {
                Toast.MakeText(this, "One of the text fields is empty", ToastLength.Short).Show();
            }
            else if (edtEmail.Text != edtConfirmEmail.Text)
            {
                Toast.MakeText(this, "Email does not match confirm email", ToastLength.Short).Show();
            }
            else if (edtPassword.Text != edtConfirmPassword.Text)
            {
                Toast.MakeText(this, "Password does not match confirm password", ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    //establish connection string 
                    //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                    //create a new instance of a db using the dpPath
                    //var db = new SQLiteConnection(dpPath);
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
                    PrintFolderPath(path);
                    //create a new table using the "LoginTable" object
                    //db.CreateTable<User>();
                    // create a new instance of a "loginTable"

                    //message += path;

                    var data = db.Table<User>();
                    //check to see if the user exsists/ the email and username matches up.
                    var data1 = data.Where(x => x.UserEmail == edtEmail.Text && x.Password == edtPassword.Text).FirstOrDefault();
                    //if there is a instance of username/password present
                    if (data1 != null)
                    {
                        Toast.MakeText(this, "User already exsists", ToastLength.Short).Show();
                    }
                    //else indicate that the user details are incorrect.
                    else
                    {
                        User newUser = new User();
                        // assign the property names with the values from the text fields
                        newUser.UserUID = new Guid();
                        newUser.FirstName = edtFirstName.Text;//edtFirstName.Text;
                        newUser.SureName = edtLastName.Text;
                        newUser.UserEmail = edtEmail.Text;
                        newUser.Password = edtConfirmPassword.Text;
                        //insert the table into the db
                        db.DeleteAll<User>();
                        
                        db.Insert(newUser);
                        string message = "Record Added Successfully at path";
                        Toast.MakeText(this, message, ToastLength.Short).Show();
                        StartActivity(typeof(MainActivity));
                    }

                    //repsonsed with message

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
            }
        }
        static void PrintFolderPath(string path)
        {
            Console.WriteLine("{0}", path);
        }

    }
}