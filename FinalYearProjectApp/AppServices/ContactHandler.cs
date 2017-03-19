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

    }
}