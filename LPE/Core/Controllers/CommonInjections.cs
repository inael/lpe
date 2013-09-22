using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;
using System.Configuration;
using System.Data.Common;
using Ninject;
using Ninject.Web.Common;
using System.Web;

namespace Core.Infrastructure
{
    public class CommonKernel : DefaultControllerFactory
    {
        public IKernel kernel = new StandardKernel(new CommonInjections());

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }
    }

    public class CommonInjections : NinjectModule
    {
        public override void Load()
        {
            //Bind<IDbConnection>().To<DatabaseGateway>().InScope(ctx => HttpContext.Current);
            //WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString);
            //Bind<IEntityRepository>().To<DatabaseUtils>();
        }
    }
}
