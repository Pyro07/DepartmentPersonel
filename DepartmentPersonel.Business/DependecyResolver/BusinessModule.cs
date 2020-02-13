using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Business.Concrete;
using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Business.DependecyResolver
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDepartmentService>().To<DepartmentManager>().InSingletonScope();
            Bind<IPersonelService>().To<PersonelManager>().InSingletonScope();
            Bind<IDepartmentDal>().To<EfDepartmentDal>().InSingletonScope();
            Bind<IPersonelDal>().To<EfPersonelDal>().InSingletonScope();
        }
    }
}
