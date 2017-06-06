using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Business.Interface;
using Core.CrossCutting;
using Core.Data.Model;
using Core.Data.Repository.Interface;
using Core.Data.WebServices.Interfaces;

namespace Core.Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWebService _userWebService;
        private readonly ConnectedStateService _сonnectedStateService;

        public UserService(IUserRepository repository, IUserWebService webService,
            ConnectedStateService сonnectedStateService)
        {
            _userRepository = repository;
            _userWebService = webService;
            _сonnectedStateService = сonnectedStateService;
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                if (await _сonnectedStateService.IsServiceAvailable())
                {
                    return await _userWebService.Login(loginModel) && await _userRepository.Login(loginModel);
                }
                return await _userRepository.Login(loginModel);

            }
            catch (VTSException)
            {
                return false;
            }
        }
    }
}