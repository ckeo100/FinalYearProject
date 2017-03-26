using System;

using Android.App;
using Android.OS;
using Android.Widget;
using FinalYearProjectApp.Model;
using FinalYearProjectApp.AppServices;
using SQLite;

namespace FinalYearProjectApp
{
    [Activity( Icon = "@drawable/JobAdIcon", MainLauncher = true)]
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
            edtPassword.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
            checkedIfTablesExsists();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var db = new SQLiteConnection(System.IO.Path.Combine(path, "user.db"));
            var userTable = db.Table<User>();
            User userdata = userTable.Where(x => x.UserEmail == edtEmailAddress.Text && x.Password == edtPassword.Text).FirstOrDefault();
            if(userdata != null)
            {
                StartActivity(typeof(MainActivity));
            }
            else
            {
                Toast.MakeText(this, "User Email or password invalid", ToastLength.Short).Show();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
        }


        private void checkedIfTablesExsists()
        {
            bool userDoesExsists = sqlhandler.checkIfUserTableExists();
            bool joblistDoesExsists = sqlhandler.checkIfJobTableExists();
            if (userDoesExsists == false)
            {
                sqlhandler.createUserDB();
                bool checkAgain = sqlhandler.checkIfUserTableExists();
            }
            if (joblistDoesExsists == false)
            {
                sqlhandler.createJobListDB();
            }
            //else
            //{
            //    sqlhandler.resetNewJobListDB();
            //}
        }
    }
}