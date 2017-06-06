using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Core.Business.Interface;
using Core.Business.ViewModels;
using Core.Data;
using Core.Data.Helpers;
using SimpleInjector;

namespace Tests
{
    public class Factory
    {
        private readonly Container _container = new Container();

        public void Init()
        {

            _container.Register<IFileHelper, FileHelper>();
            new DataRegister().Register(_container);
            new BusinessRegister().Register(_container);

            _container.Verify();
        }

        public T Get<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
