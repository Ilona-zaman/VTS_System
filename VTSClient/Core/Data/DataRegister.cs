using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Repository.Implementation;
using Core.Data.Repository.Interface;
using Core.Data.WebServices;
using Core.Data.WebServices.Interfaces;
using SimpleInjector;

namespace Core.Data
{
    public class DataRegister
    {
        public void Register(Container container)
        {
            container.Register<IUserRepository, UserRepository>();
            container.Register<IVacationRepository, VacationRepository>();
            container.Register<IVacationWebService, VacationWebService>();
            container.Register<IUserWebService, UserWebService>();
        }
    }
}
