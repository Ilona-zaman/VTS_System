using System;
using Cirrious.FluentLayouts.Touch;
using Core.Business.ViewModels;
using UIKit;
using Core.Business.Interface;
using System.ComponentModel;
using System.Drawing;

namespace iOS.Controllers
{
    public partial class LoginViewController : UIViewController
    {
        private UILabel _errorLabel;
        private UITextField _userName;
        private UITextField _password;
        private UIButton _loginButton;
        private readonly UserViewModel _userViewModel;
        private VacationViewModel _vacationViewModel;
        private ILocalizationService _localizationService;
        public LoginViewController()
        {
            _userViewModel = AppDelegate.UserViewModel;
            _vacationViewModel = AppDelegate.VacationViewModel;
            _localizationService = AppDelegate.LocalizationService;
        }

        public override void ViewWillAppear(bool animated)
        {
            _vacationViewModel.PropertyChanged += ViewModelOnPropertyChanged;
            _userName.EditingDidEndOnExit += HideKeyboard;
            _password.EditingDidEndOnExit += HideKeyboard;
            _loginButton.TouchUpInside += Login;

        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _userName.EditingDidEndOnExit -= HideKeyboard;
            _password.EditingDidEndOnExit -= HideKeyboard;
            _loginButton.TouchUpInside -= Login;
            _vacationViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TabBarItem.Enabled = false;
            View.BackgroundColor = UIColor.FromRGB(230, 229, 228);

            NavigationController.NavigationBarHidden = true;

            UIImageView _logo = new UIImageView(UIImage.FromBundle("Login_bg"));
            
            Add(_logo);

            _errorLabel = new UILabel();
            _errorLabel.TextColor = UIColor.Red;
            _errorLabel.TextAlignment = UITextAlignment.Center;
            _errorLabel.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            _errorLabel.Hidden = true;
            Add(_errorLabel);

            _userName = new UITextField();
            _userName.TextColor = UIColor.Black;
            _userName.Text = "ark";
            _userName.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            Add(_userName);

            _password = new UITextField();
            _password.TextColor = UIColor.Black;
            _password.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            _password.SecureTextEntry = true;
            _password.Text = "123";
            Add(_password);

            _loginButton = new UIButton();
            _loginButton.SetTitle(_localizationService.Localize("ButtonLogin"), UIControlState.Normal);
            _loginButton.SetTitleColor(UIColor.FromRGB(255, 255, 255), UIControlState.Normal);
            _loginButton.BackgroundColor = UIColor.FromRGB(57, 197, 214);
            Add(_loginButton);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(
                _logo.WithSameCenterX(View),
                _logo.WithSameCenterY(View),
                _logo.Height().EqualTo().HeightOf(View),
                _logo.Width().EqualTo().WidthOf(View),

                _errorLabel.WithSameCenterX(View),
                _errorLabel.WithSameCenterY(View).Minus(100),
                _errorLabel.WithSameWidth(View).Minus(50),
                _errorLabel.Height().EqualTo(40),

                _userName.Below(_errorLabel, 20),
                _userName.WithSameCenterX(_errorLabel),
                _userName.WithSameWidth(_errorLabel),
                _userName.WithSameHeight(_errorLabel),

                _password.Below(_userName, 20),
                _password.WithSameCenterX(_userName),
                _password.WithSameWidth(_userName),
                _password.WithSameHeight(_userName),

                _loginButton.Below(_password, 20),
                _loginButton.WithSameCenterX(_userName),
                _loginButton.WithSameWidth(_userName).Minus(40),
                _loginButton.WithSameHeight(_userName)
                );
        }

        private async void Login(object sender, EventArgs e)
        {
            var isLogin = await _userViewModel.Login(_userName.Text, _password.Text);
            if (isLogin)
            {
                await _vacationViewModel.Init(_userViewModel);
                SplitViewController splitViewController = new SplitViewController(new VacationsViewController(0));
                Context.App.Window.RootViewController = splitViewController;
                Context.App.Window.MakeKeyAndVisible();
            }
            else
            {
                _errorLabel.Hidden = false;
                _errorLabel.Text = _localizationService.Localize("AutorizationFaild");
            }
        }

        private void HideKeyboard(object sender, EventArgs e)
        {
            ((UITextField)sender).ResignFirstResponder();
        }

        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }

    public static class Context
    {
        public static AppDelegate App = UIApplication.SharedApplication.Delegate as AppDelegate;
    }
}