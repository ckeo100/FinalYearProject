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
using FinalYearProjectClassLibrary;
using FinalYearProjectApp.Model;
//using FinalYearProjectClassLibrary.Model;

namespace FinalYearProjectApp.Adaptors
{
    public class JobListAdaptor: BaseAdapter<UserPotentialJob>
    {
        List<UserPotentialJob> items;
        Activity context;

        public JobListAdaptor(Activity context, List<UserPotentialJob> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
            //throw new NotImplementedException();
        }

        public override UserPotentialJob this[int position]
        {
            get
            {
                //throw new NotImplementedException();
                return items[position];
            }
        }

        public override int Count
        {
            
            get
            {
                
                return items.Count;
            }
        }


        //if there are view rows that are not being used (shown on screen) the reuse them as convertView
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            //if there is no row view that are not being being used, create a new row view
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            //assigns the row text to the text of the current item jobName
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.jobName;
            //convertView.FindViewById<TextView>(Android.Resource.).Text = item.JobID.ToString();
            return convertView;
        }
    }
    
}