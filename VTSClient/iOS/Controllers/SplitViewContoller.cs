using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace iOS.Controllers
{
    class SplitViewController : UISplitViewController
    {
    public SplitViewController(UIViewController controller) :base()
        {
            MenuViewController menuView = new MenuViewController();

            ViewControllers = new UIViewController[] { menuView, new UINavigationController(controller) };
            menuView.RowClicked += (object sender, MenuViewController.RowClickedEventArgs e) =>
            {
                switch (e.Item)
                {
                    case 0:
                        {
                            this.ShowViewController(new UINavigationController(new VacationsViewController(0)), this);
                            break;
                        }
                    case 1:
                        {
                            this.ShowViewController(new UINavigationController(new VacationsViewController(1)), this);
                            break;
                        }
                    case 2:
                        {
                            this.ShowViewController(new UINavigationController(new VacationsViewController(2)), this);
                            break;
                        }

                    default:
                        {
                            this.ShowViewController(new UINavigationController(new VacationsViewController(2)), this);
                            break;
                        }
                }
            };

            WillHideViewController += (object sender, UISplitViewHideEventArgs e) =>
            {
            };

            WillShowViewController += (object sender, UISplitViewShowEventArgs e) =>
            {
            };

            ShouldHideViewController = (svc, viewController, inOrientation) => 
            {
                return inOrientation == UIInterfaceOrientation.Portrait ||
                    inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
            };
        }

        public override bool ShouldAutorotate()
        {
            return true;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.All;
        }
    }
}