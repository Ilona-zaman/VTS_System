using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Widget;
using Core.Business.ViewModels;
using Core.Data.Model;
using Droid.Adapters;
using Droid.Helpers;

namespace Droid.Activities
{
    [Activity(Label = "Request", Theme = "@style/DetailStyle")]
    public class DetailsActivity : AppCompatActivity
    {
        private Vacation _vacation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            var vacationViewModel = ((AndroidApp) Application).VacationViewModel;
            var localizationService = ((AndroidApp)Application).LocalizationService;
            vacationViewModel.PropertyChanged += ViewModelOnPropertyChanged;
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Delail);

            Android.Support.V7.Widget.Toolbar toolbar =
                FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(true);


            Button saveButton = FindViewById<Button>(Resource.Id.saveButton);
            Button startDate = FindViewById<Button>(Resource.Id.startDate);
            Button endDate = FindViewById<Button>(Resource.Id.endDate);

            string vacId = Intent.GetStringExtra("Vacation");

            if (vacId == null)
            {
                _vacation = new Vacation();
                _vacation.Id = new Guid();
                _vacation.Created = DateTime.Now;
                _vacation.CreatedBy = "Name";
                _vacation.Start = _vacation.Created;
                _vacation.End = _vacation.Created.AddDays(1);
                _vacation.VacationTypeId = 1;
                _vacation.VacationStatusId = 3;
            }
            else
            {
                _vacation = vacationViewModel.FindVacation(vacId);
            }

            startDate.Text = _vacation.Start.ToString("d MMM yyyy");
            endDate.Text = _vacation.End.ToString("d MMM yyyy");

            RadioButton approved = FindViewById<RadioButton>(Resource.Id.approved);
            approved.Text = localizationService.Localize("Open");
            RadioButton closed = FindViewById<RadioButton>(Resource.Id.closed);
            closed.Text = localizationService.Localize("Close");
            if (_vacation.VacationStatusId == 3)
            {
                approved.Checked = true;
            }
            else
            {
                closed.Checked = true;
            }
            approved.Click += (sender, e) =>
            {
                if (((RadioButton) sender).Id == Resource.Id.approved)
                {
                    _vacation.VacationStatusId = 3;
                }
                else
                {
                    _vacation.VacationStatusId = 5;
                }
            };
            closed.Click += (sender, e) =>
            {
                if (((RadioButton) sender).Id == Resource.Id.closed)
                {
                    _vacation.VacationStatusId = 5;
                }
                else
                {
                    _vacation.VacationStatusId = 3;
                }
            };


            startDate.Click += (sender, eventArgs) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time)
                {
                    _vacation.Start = time;
                    startDate.Text = time.ToString("d MMM yyyy");
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            endDate.Click += (sender, eventArgs) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time)
                {
                    _vacation.End = time;
                    endDate.Text = time.ToString("d MMM yyyy");
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            ViewPager viewPager;
            DetailFragmentStatePagerAdapter detailFragmentPagerAdapter;

            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            detailFragmentPagerAdapter = new DetailFragmentStatePagerAdapter(SupportFragmentManager);
            detailFragmentPagerAdapter.addFragmentView((arg1, arg2, arg3) =>
            {
                var view = arg1.Inflate(Resource.Layout.ImageViewLayout, arg2, false);
                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                imageView.SetImageResource(Resource.Drawable.Icon_Request_Green);
                var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
                vacationType.Text = localizationService.Localize("Regular");
                return view;
            });
            detailFragmentPagerAdapter.addFragmentView((arg1, arg2, arg3) =>
            {
                var view = arg1.Inflate(Resource.Layout.ImageViewLayout, arg2, false);
                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                imageView.SetImageResource(Resource.Drawable.Icon_Request_Plum);
                var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
                vacationType.Text = localizationService.Localize("Sick");
                return view;
            });
            detailFragmentPagerAdapter.addFragmentView((arg1, arg2, arg3) =>
            {
                var view = arg1.Inflate(Resource.Layout.ImageViewLayout, arg2, false);
                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                imageView.SetImageResource(Resource.Drawable.Icon_Request_Gray);
                var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
                vacationType.Text = localizationService.Localize("Exceptional");
                return view;
            });
            detailFragmentPagerAdapter.addFragmentView((arg1, arg2, arg3) =>
            {
                var view = arg1.Inflate(Resource.Layout.ImageViewLayout, arg2, false);
                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                imageView.SetImageResource(Resource.Drawable.Icon_Request_Dark);
                var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
                vacationType.Text = localizationService.Localize("LeaveWithoutPay");
                return view;
            });
            detailFragmentPagerAdapter.addFragmentView((arg1, arg2, arg3) =>
            {
                var view = arg1.Inflate(Resource.Layout.ImageViewLayout, arg2, false);
                var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                imageView.SetImageResource(Resource.Drawable.Icon_Request_Blue);
                var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
                vacationType.Text = localizationService.Localize("Overtime");
                return view;
            });
            
            viewPager.Adapter = detailFragmentPagerAdapter;
            viewPager.SetCurrentItem(_vacation.VacationTypeId - 1, false);

            saveButton.Click += async (sender, a) =>
            {
                if (vacId == null)
                {
                    _vacation.VacationTypeId = viewPager.CurrentItem + 1;
                    await vacationViewModel.AddVacation(_vacation);
                }
                _vacation.VacationTypeId = viewPager.CurrentItem + 1;
                _vacation.Created = DateTime.Now;
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("Filter", "ALL");
                StartActivity(intent);
            };

            vacationViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }

    public class DatePickerFragment : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                this,
                currently.Year,
                currently.Month - 1,
                currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            _dateSelectedHandler(selectedDate);
        }
    }
}