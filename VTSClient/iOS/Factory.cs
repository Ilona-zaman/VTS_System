using Core.Business;
using Core.Data;
using Core.Data.Helpers;
using iOS.Helpers;
using Container = SimpleInjector.Container;

namespace iOS
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
