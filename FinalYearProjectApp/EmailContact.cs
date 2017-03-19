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
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp
{
    [Activity(Label = "EmailContact")]
    public class EmailContact : Activity
    {
        public UserModel userModel;
        public EditText edtSendToAddressline;
        public EditText edtSendFromAddressline;
        public EditText edtEmailSubject;
        public EditText edtBodyText;
        public Button btnSendButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userModel = new UserModel();
            User userDetails = userModel.getCurrentUser();
            SetContentView(Resource.Layout.EmailContact);
            edtSendToAddressline = FindViewById<EditText>(Resource.Id.edtSendToEmailAddressLine);
            edtSendFromAddressline = FindViewById<EditText>(Resource.Id.edtSendFromEmailAddressLine);
            edtEmailSubject = FindViewById<EditText>(Resource.Id.edtSubjectLine);
            edtBodyText = FindViewById<EditText>(Resource.Id.edtBodyText);
            btnSendButton = FindViewById<Button>(Resource.Id.btnSendEmail);
            edtSendToAddressline.Text = Intent.Extras.GetString("employeeEmail");
            edtSendFromAddressline.Text = userDetails.UserEmail;
            btnSendButton.Click += btnSendButton_Click;
            // Create your application here
        }

        private void btnSendButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Intent email = new Intent(Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { edtSendToAddressline.Text.ToString() });
            email.PutExtra(Intent.ExtraSubject, edtEmailSubject.Text.ToString());
            email.PutExtra(Intent.ExtraText, edtBodyText.Text.ToString());
            email.SetType("message/rfc822");
            StartActivity(Intent.CreateChooser(email, "Send Email"));
        }
    }
}