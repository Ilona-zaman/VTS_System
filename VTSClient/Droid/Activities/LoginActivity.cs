using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Core.Business.Implementation;
using Core.Business.ViewModels;
using Core.CrossCutting;
using Core.Data.Helpers;

namespace Droid.Activities
{
    [Activity(Theme = "@style/Base.Theme.Login")]
    public class LoginActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var dbService = ((AndroidApp) Application).Factory.Get<DBService>();
            var filehelper = ((AndroidApp) Application).Factory.Get<IFileHelper>();
            dbService.CreateDatabase(filehelper.GetDBPath(Configuration.DatabaseName));

            var userViewModel = ((AndroidApp) Application).UserViewModel;
            var vacationViewModel = ((AndroidApp) Application).VacationViewModel;
            var localizationService = ((AndroidApp) Application).LocalizationService;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            EditText login = FindViewById<EditText>(Resource.Id.loginText);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            TextView text = FindViewById<TextView>(Resource.Id.LoginError);
            text.Visibility = ViewStates.Invisible;

            var button = FindViewById<Button>(Resource.Id.login);
            button.Text = localizationService.Localize("ButtonLogin");

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            button.Click += async (sender, e) =>
            {
                progressBar.Visibility = ViewStates.Visible;
                var isLogin = await userViewModel.Login(login.Text, password.Text);
                if (isLogin)
                {
                    text.Visibility = ViewStates.Invisible;
                    await vacationViewModel.Init(userViewModel);
                    var intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("Filter", "ALL");
                    StartActivity(intent);
                }
                else
                {
                    text.Visibility = ViewStates.Visible;
                    text.Text = localizationService.Localize("AutorizationFaild");
                }
                progressBar.Visibility = ViewStates.Invisible;
            };
        }
    }
}