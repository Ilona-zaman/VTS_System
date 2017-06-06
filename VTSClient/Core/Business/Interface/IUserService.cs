using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Business.Interface
{
    public interface IUserService
    {
        Task<bool> Login(LoginModel loginModel);
    }
}