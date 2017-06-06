using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Data.WebServices.Interfaces
{
    public interface IUserWebService
    {
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Ping();
    }
}
