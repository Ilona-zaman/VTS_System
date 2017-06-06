using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Core.Data.Model;

namespace Droid.Adapters
{
    internal class VTSListAdapter : BaseAdapter<Vacation>
    {
        private readonly Activity context;
        private readonly List<Vacation> items;

        public VTSListAdapter(Activity context, List<Vacation> items)
        {
            this.context = context;
            this.items = items;
        }

        public override Vacation this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var localizationService = ((AndroidApp)context.Application).LocalizationService;
            var item = items[position];
            var view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItemVacation, null);
            view.FindViewById<TextView>(Resource.Id.Date).Text = item.Start.ToString("MMM d") + "-" +
                                                                 item.End.ToString("MMM d");
            var imageStatus = view.FindViewById<ImageView>(Resource.Id.vacType);

            if (item.VacationStatusId == 3)
            {
                view.FindViewById<TextView>(Resource.Id.vacationStatus).Text = localizationService.Localize("Open");
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.vacationStatus).Text = localizationService.Localize("Close");
            }

            if (item.VacationTypeId == 1)
            {
                imageStatus.SetImageResource(Resource.Drawable.Icon_Request_Green);
                view.FindViewById<TextView>(Resource.Id.vacationType).Text = localizationService.Localize("Regular");
            }
            if (item.VacationTypeId == 2)
            {
                imageStatus.SetImageResource(Resource.Drawable.Icon_Request_Plum);
                view.FindViewById<TextView>(Resource.Id.vacationType).Text = localizationService.Localize("Sick");
            }
            if (item.VacationTypeId == 3)
            {
                imageStatus.SetImageResource(Resource.Drawable.Icon_Request_Gray);
                view.FindViewById<TextView>(Resource.Id.vacationType).Text = localizationService.Localize("Exceptional");
            }
            if (item.VacationTypeId == 4)
            {
                imageStatus.SetImageResource(Resource.Drawable.Icon_Request_Dark);
                view.FindViewById<TextView>(Resource.Id.vacationType).Text = localizationService.Localize("LeaveWithoutPay");
            }
            if (item.VacationTypeId == 5)
            {
                imageStatus.SetImageResource(Resource.Drawable.Icon_Request_Blue);
                view.FindViewById<TextView>(Resource.Id.vacationType).Text = "Overtime";
            }
            return view;
        }
    }
}