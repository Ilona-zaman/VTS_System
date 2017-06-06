using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business.Implementation;
using Core.Business.Interface;
using Core.Business.ViewModels;
using SimpleInjector;

namespace Core.Business
{
    public class BusinessRegister
    {
        public void Register(Container container)
        {
            container.Register<DBService>();
            container.Register<IUserService, UserService>();
            container.Register<IVacationService, VacationService>();
            container.Register<ILocalizationService, LocalizationService>(Lifestyle.Singleton);
            container.Register<VacationViewModel>();
            container.Register<UserViewModel>();
            container.Register<ConnectedStateService>();
        }
    }
}
