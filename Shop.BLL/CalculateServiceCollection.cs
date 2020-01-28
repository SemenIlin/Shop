using Ninject.Modules;
using Shop.DAL.Interfaces;
using Shop.DAL.Repositories;

namespace Shop.BLL
{
    public class CalculateServiceCollection : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<ListUnitOfWorks>();          
        }
    }
}
