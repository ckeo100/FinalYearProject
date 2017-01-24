using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace FinalYearProject
{
    [Activity(Label = "FinalYearProject", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private string translatedNumber = string.Empty;
        private EditText phoneNumberText;
        private Button translateButton, callButton; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //find control 
            phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            translateButton = FindViewById<Button>(Resource.Id.btnTranslate);
            callButton = FindViewById<Button>(Resource.Id.btnCall);

            callButton.Enabled = false;
            translateButton.Click += TranslateButton_Click;
            callButton.Click += callButton_Click;

        }

        private void callButton_Click(object sender, EventArgs e)
        {
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("Call" + translateButton + "?");
            callDialog.SetNeutralButton("Call", delegate {

                var callIntent = new intent(Intent.ActionCall);
                ca
            });
        }

        private void TranslateButton_Click(object sender, EventArgs e)
        {
            translatedNumber = PhoneTranslator.ToNumber(phoneNumberText.Text);
            if(string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.Text = "Call";
                callButton.Enabled = false;
            }
            else
            {
                callButton.Text = "Call"+ translatedNumber;
                callButton.Enabled = true;
            }
        }
    }
}

