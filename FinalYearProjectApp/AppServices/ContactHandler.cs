using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalYearProjectApp.Model;

namespace FinalYearProjectApp.AppServices
{
    class ContactHandler
    {
        public ContactHandler()
        {
  
        }

        public bool IsContactEmailAddress(string contactDetails)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(contactDetails);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void sendEmailList(List<UserPotentialJob> UserJobAd)
        {
            UserModel usermodel = new UserModel();
            User userDetails = usermodel.getCurrentUser();

            MailMessage mail = new MailMessage("ciarankeogh100@gmail.com", userDetails.UserEmail);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "test";
            mail.Body = "test";
            client.Send(mail);

        }


    }
}