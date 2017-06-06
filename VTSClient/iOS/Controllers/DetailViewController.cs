using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cirrious.FluentLayouts.Touch;
using Core.Business.Interface;
using Core.Business.ViewModels;
using Core.Data.Model;
using CoreGraphics;
using Foundation;
using MonoTouch.Dialog;
using UIKit;

namespace iOS.Controllers
{
    public class DetailViewController : UIViewController
    {
        private VacationViewModel _vacationViewModel;
        private ILocalizationService _localizationService;
        private Vacation _vacation;
        public string Id { get; set; }

        public DetailViewController() 
        {
            _vacationViewModel = AppDelegate.VacationViewModel;
            _localizationService = AppDelegate.LocalizationService;
        }

        public override void ViewWillAppear(bool animate)
        {
            base.ViewDidAppear(animate);
            _vacationViewModel.PropertyChanged += ViewModelOnPropertyChanged;

        }
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _vacationViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            UILabel label = new UILabel();
            UIBarButtonItem save = new UIBarButtonItem(UIBarButtonSystemItem.Save, ButnSaveClicked);
            NavigationItem.RightBarButtonItem = save;

            
            if (Id == null)
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
                _vacation = _vacationViewModel.FindVacation(Id);
            }


            var controllers = new List<UIViewController>
            {
                new TypeVacViewController(View, 1),
                new TypeVacViewController(View, 2),
                new TypeVacViewController(View, 3),
                new TypeVacViewController(View, 4)
            };

            var _pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
            var dataSource = new ViewDataSource(controllers);
            _pageViewController.DataSource = dataSource;
            _pageViewController.SetViewControllers(new[] { controllers[0] }, UIPageViewControllerNavigationDirection.Forward, false, null);
            View.AddSubview(_pageViewController.View);

            

            UIDatePicker dateBtn = new UIDatePicker();
            dateBtn.Mode = UIDatePickerMode.Date;
            dateBtn.Hidden = true;
            Add(dateBtn);

            
            
            UIButton uiStartButton = new UIButton();
            uiStartButton.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            uiStartButton.SetTitle(_vacation.Start.ToString("d MMM yyyy"), UIControlState.Normal);
            uiStartButton.SetTitleColor(UIColor.FromRGB(160, 204, 75), UIControlState.Normal);
            Add(uiStartButton);

            UIButton uiEndButton = new UIButton();
            uiEndButton.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            uiEndButton.SetTitle(_vacation.End.ToString("d MMM yyyy"), UIControlState.Normal);
            uiEndButton.SetTitleColor(UIColor.FromRGB(57, 197, 214), UIControlState.Normal);
            Add(uiEndButton);

            UISegmentedControl segmentControl = new UISegmentedControl
            {
                Frame = new CGRect(20, 20, 280, 40)
            };
            segmentControl.InsertSegment(_localizationService.Localize("Open"), 0, false);
            segmentControl.InsertSegment(_localizationService.Localize("Close"), 1, false);
            segmentControl.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            segmentControl.TintColor = UIColor.FromRGB(160, 204, 75);
            if (_vacation.VacationStatusId == 3)
            {
                segmentControl.SelectedSegment = 0;
            }
            else
            {
                segmentControl.SelectedSegment = 1;
            }

            segmentControl.ValueChanged += (sender, e) => {
                var selectedSegmentId = 
                (sender as UISegmentedControl).SelectedSegment;
                                                              if (selectedSegmentId == 0)
                                                              {
                                                                  _vacation.VacationStatusId = 3;
                                                              }
                                                              else
                                                              {
                                                                  _vacation.VacationStatusId = 5;
                                                              }
            };

            UISegmentedControl segmentControlImege = new UISegmentedControl
            {
                Frame = new CGRect(20, 20, 280, 40)
            };
            segmentControlImege.InsertSegment(UIImage.FromBundle("Icon_Request_Green"), 0, true);
            segmentControlImege.InsertSegment(UIImage.FromBundle("Icon_Request_Blue"), 1, true);
            segmentControlImege.InsertSegment(UIImage.FromBundle("Icon_Request_Gray"), 2, true);
            segmentControlImege.InsertSegment(UIImage.FromBundle("Icon_Request_Gray"), 3, true);
            segmentControlImege.InsertSegment(UIImage.FromBundle("Icon_Request_Gray"), 4, true);
            segmentControlImege.BackgroundColor = UIColor.Clear;
            segmentControlImege.TintColor = UIColor.FromRGB(160, 204, 75);
            if (_vacation.VacationTypeId == 1)
            {
                segmentControlImege.SelectedSegment = 0;
            }
            else
            {
                segmentControlImege.SelectedSegment = 1;
            }

            segmentControlImege.ValueChanged += (sender, e) => {
                var selectedSegmentId =
                (sender as UISegmentedControl).SelectedSegment;
                if (selectedSegmentId == 0)
                {
                    _vacation.VacationTypeId =1;
                }
                if (selectedSegmentId == 1)
                {
                    _vacation.VacationTypeId = 2;
                }
                if (selectedSegmentId == 2)
                {
                    _vacation.VacationTypeId = 3;
                }
                if (selectedSegmentId == 4)
                {
                    _vacation.VacationTypeId = 5;
                }
                else
                {
                    _vacation.VacationTypeId = 1;
                }
            };

            Add(segmentControlImege);

            uiStartButton.TouchUpInside += (sender, arg) =>
            {
                dateBtn.Hidden = false;
                dateBtn.ValueChanged += (s, e) =>
                {
                    DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
                    _vacation.Start = reference.AddSeconds(dateBtn.Date.SecondsSinceReferenceDate);
                    uiStartButton.SetTitle(_vacation.Start.ToString("d MMM yyyy"), UIControlState.Normal);
                    dateBtn.Hidden = true;
                };
            };

            uiEndButton.TouchUpInside += (sender, arg) =>
            {
                dateBtn.Hidden = false;
                dateBtn.ValueChanged += (s, e) =>
                {
                    DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
                    _vacation.End = reference.AddSeconds(dateBtn.Date.SecondsSinceReferenceDate);
                    uiEndButton.SetTitle(_vacation.End.ToString("d MMM yyyy"), UIControlState.Normal);
                    dateBtn.Hidden = true;
                };
            };

            Add(label);
            Add(segmentControl);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(

                uiStartButton.Left().EqualTo().LeftOf(View).Plus(20),
                uiStartButton.Right().EqualTo().RightOf(uiEndButton).Minus(10),
                uiStartButton.WithSameCenterY(View).Minus(150),
                uiStartButton.WithSameWidth(View).Minus(View.Center.X).Minus(30),
                uiStartButton.Height().EqualTo(40),

                uiEndButton.Left().EqualTo().RightOf(uiStartButton).Plus(10),
                uiEndButton.Right().EqualTo().RightOf(View).Minus(20),
                uiEndButton.WithSameCenterY(View).Minus(150),
                uiEndButton.WithSameWidth(View).Minus(View.Center.X).Minus(30),
                uiEndButton.Height().EqualTo(40),

                segmentControl.Below(uiStartButton, 20),
                segmentControl.WithSameCenterX(View),
                segmentControl.Width().EqualTo(180),
                segmentControl.Height().EqualTo(40),

                segmentControlImege.Below(segmentControl, 20),
                segmentControlImege.WithSameCenterX(View),
                segmentControlImege.Width().EqualTo(250),
                segmentControlImege.Height().EqualTo(40),

                dateBtn.AtBottomOf(View).Minus(80),
                dateBtn.WithSameCenterX(View),
                dateBtn.Width().EqualTo(UIScreen.MainScreen.Bounds.Width).Minus((UIScreen.MainScreen.Bounds.Width / 9) * 2),
                dateBtn.Height().EqualTo(120)
                );

        }
        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }

        async void ButnSaveClicked(object sender, EventArgs e)
        {
            if (Id != null)
            {
                await _vacationViewModel.UpdateVacation(_vacation);
                NavigationController.PopToRootViewController(false);
            }
            else
            {
                await _vacationViewModel.AddVacation(_vacation);
                NavigationController.PopToRootViewController(false);
            }
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }

    public class TypeVacViewController : UIViewController
    {
        private readonly UIView _parent;
        private readonly int _number;
        public TypeVacViewController(UIView parent, int number)
        {
            _parent = parent;
            _number = number;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var textView = new UITextView(_parent.Bounds);
            textView.TextAlignment = UITextAlignment.Center;
            textView.AttributedText = new NSAttributedString(_number.ToString(), UIFont.BoldSystemFontOfSize(18));
            View.Add(textView);
        }
    }

    public class ViewDataSource : UIPageViewControllerDataSource
    {
        private LinkedListNode<UIViewController> current;
        LinkedList<UIViewController> controllerLiist;

        public ViewDataSource(IEnumerable<UIViewController> controllers)
        {
            controllerLiist = new LinkedList<UIViewController>(controllers);
            current = controllerLiist.First;
        }

        public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var __c = controllerLiist.Find(referenceViewController);

            if (__c.Previous != null)
                return __c.Previous.Value;
            else
                return null;
        }
        public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var __c = controllerLiist.Find(referenceViewController);

            if (__c.Next != null)
                return __c.Next.Value;
            else
                return null;
        }
    }
}