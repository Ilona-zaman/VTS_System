using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business.Implementation;
using Core.Business.Interface;
using Core.CrossCutting;
using Core.Data.Model;

namespace Core.Business.ViewModels
{
    public class UserViewModel
    {
        private readonly IUserService _userService;
        public bool IsLogin { get; set; }

        public UserViewModel(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException(nameof(userService));
            _userService = userService;
        }

        public async Task<bool> Login(string login, string password)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Login = login;
                loginModel.Password = password;
                IsLogin = await _userService.Login(loginModel);
                return IsLogin;
            }
            catch (VTSException)
            {
                return false;
            }
        }

    }
}
