﻿using Core.Business.Implementation;
using Core.Business.Interface;
using Core.Business.ViewModels;
using Core.CrossCutting;
using Core.Data.Helpers;
using Foundation;
using iOS.Controllers;
using System.Globalization;
using UIKit;

namespace iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public static Factory Factory { private set; get; }
        public static VacationViewModel VacationViewModel { private set; get; }
        public static UserViewModel UserViewModel { private set; get; }
        public static ILocalizationService LocalizationService { private set; get; }
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Factory = new Factory();
            Factory.Init();
            var dbService = Factory.Get<DBService>();
            var filehelper = Factory.Get<IFileHelper>();
            dbService.CreateDatabase(filehelper.GetDBPath(Configuration.DatabaseName));

            VacationViewModel = Factory.Get<VacationViewModel>();
            UserViewModel = Factory.Get<UserViewModel>();
            LocalizationService = Factory.Get<ILocalizationService>();
            LocalizationService.CultureInfo = new CultureInfo("en");

            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // If you have defined a root view controller, set it here:
            // Window.RootViewController = myViewController;
            Window.RootViewController = new UINavigationController(new LoginViewController());

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

