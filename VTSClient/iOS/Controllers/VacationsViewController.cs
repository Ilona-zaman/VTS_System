using Core.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using Core.Data.Model;
using Foundation;
using iOS.Helpers;
using UIKit;

namespace iOS.Controllers
{
    public partial class VacationsViewController : UIViewController
    {
        private UITableView table;
        private VacationViewModel _vacationsViewModel;
        private UIBarButtonItem _butnAdd, _butnMenu;
        private int _filter;

        public VacationsViewController(int filter)
        {
            _filter = filter;
            _vacationsViewModel = AppDelegate.VacationViewModel;
        }
        
        public override void ViewWillAppear(bool animate)
        {
            base.ViewDidAppear(animate);
            _butnMenu.Clicked += ButnMenuClicked;
            _vacationsViewModel.PropertyChanged += ViewModelOnPropertyChanged;

        }
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _butnMenu.Clicked -= ButnMenuClicked;
            //_vacationsViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            table = new UITableView(View.Bounds, UITableViewStyle.Plain);
            Add(table);
            var sourse = CreateTableSource(_filter);
            sourse.ItemClicked += (id) =>
            {
                DetailViewController detailViewController = new DetailViewController();
                detailViewController.Id = id;
                NavigationController.PushViewController(detailViewController, true);
            };

            table.Source = sourse;

            _butnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, ButnAddClicked);
            NavigationItem.RightBarButtonItem = _butnAdd;
            _butnMenu = new UIBarButtonItem();
            _butnMenu.Title = "Menu";

            NavigationItem.LeftBarButtonItem = _butnMenu;
            NavigationItem.Title = "All Requests";
            NavigationController.NavigationBar.BackgroundColor = UIColor.FromRGB(57, 197, 214);
        }
        void ButnAddClicked(object sender, EventArgs e)
        {
            DetailViewController detailViewController = new DetailViewController();
            detailViewController.Id = null;
            NavigationController.PushViewController(detailViewController, false);
        }

        void ButnMenuClicked(object sender, EventArgs e)
        {
            Context.App.Window.RootViewController = new SplitViewController(new VacationsViewController(0));
            Context.App.Window.MakeKeyAndVisible();
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }

        private TableSource CreateTableSource(int filter)
        {
            if (_filter == 0)
            {
                return new TableSource(_vacationsViewModel.Vacation, filter);
            }
            if (_filter == 1)
            {
                return new TableSource(_vacationsViewModel.Vacation.Where(vac => vac.VacationStatusId == 3).ToList(), filter);
            }
            if (_filter == 2)
            {
                return new TableSource(_vacationsViewModel.Vacation.Where(vac => vac.VacationStatusId == 5).ToList(), filter);
            }
            return null;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            table.ReloadData();
        }
    }
}