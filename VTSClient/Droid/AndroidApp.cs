using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.Business.Implementation;
using Core.Business.ViewModels;
using Core.CrossCutting;
using Core.Data.Helpers;
using Core.Business.Interface;

namespace Droid
{
    [Application]
    public class AndroidApp: Application
    {
        public Factory Factory { private set; get; }
        public VacationViewModel VacationViewModel { private set; get; }
        public UserViewModel UserViewModel { private set; get; }
        public ILocalizationService LocalizationService { private set; get; }

        public AndroidApp(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Factory = new Factory();
            Factory.Init();

            VacationViewModel = Factory.Get<VacationViewModel>();
            UserViewModel = Factory.Get<UserViewModel>();
            LocalizationService = Factory.Get<ILocalizationService>();
            LocalizationService.CultureInfo = new CultureInfo("en");
            //LocalizationService.CultureInfo = new CultureInfo("ru"); 
        }
    }
}