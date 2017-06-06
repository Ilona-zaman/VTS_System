using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> Login(LoginModel loginModel);
    }
}
