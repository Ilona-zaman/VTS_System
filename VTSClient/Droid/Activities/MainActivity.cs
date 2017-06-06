using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Core.Business.ViewModels;
using Droid.Adapters;
using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Droid.Activities
{
    [Activity(Label = "All Requests", MainLauncher = true, Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        private ListView _listView;
        private VacationViewModel _vacationViewModel;
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var localizationService = ((AndroidApp) Application).LocalizationService;
            _vacationViewModel = ((AndroidApp) Application).VacationViewModel;
            _vacationViewModel.PropertyChanged += ViewModelOnPropertyChanged;

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            _listView = FindViewById<ListView>(Resource.Id.listVac);

            var floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.addVacButton);

            floatingActionButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(DetailsActivity));
                StartActivity(intent);
            };


            var listFilter = FindViewById<ListView>(Resource.Id.listFilter);
            IList<string> filters = new List<string>
            {
                "ALL",
                "OPEN",
                "CLOSED"
            };
            listFilter.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, filters);

            var filter = Intent.GetStringExtra("Filter");
            if (filter == "ALL")
            {
                _listView.Adapter = new VTSListAdapter(this, _vacationViewModel.Vacation.ToList());
            }
            if (filter == "OPEN")
            {
                _listView.Adapter = new VTSListAdapter(this,
                    _vacationViewModel.Vacation.Where(vac => vac.VacationStatusId == 3).ToList());
            }
            if (filter == "CLOSED")
            {
                _listView.Adapter = new VTSListAdapter(this,
                    _vacationViewModel.Vacation.Where(vac => vac.VacationStatusId == 5).ToList());
            }

            _listView.ItemClick += (sender, e) =>
            {
                var intent = new Intent(this, typeof(DetailsActivity));
                intent.PutExtra("Vacation", _vacationViewModel.Vacation[e.Position].Id.ToString());
                StartActivity(intent);
            };

            listFilter.ItemClick += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("Filter", filters[e.Position]);
                StartActivity(intent);
            };

            var drawerToggle = new SupportActionBarDrawerToggle(this, drawerLayout, toolbar,
                Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            _vacationViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        protected override void OnRestart()
        {
            OnResume();
            _listView.Adapter = new VTSListAdapter(this, _vacationViewModel.Vacation.ToList());
        }

        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }
}