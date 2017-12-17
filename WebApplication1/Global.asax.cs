using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Infrastructure;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver());
          
            
            //程序启动注入
            var builder = new ContainerBuilder();
            HttpConfiguration config =GlobalConfiguration.Configuration;

            SetupResolveRules(builder);

            ////注册所有的Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //注册所有的ApiControllers
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var container = builder.Build();
            //注册api容器需要使用HttpConfiguration对象
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            var iRepository = Assembly.Load("Practice.Interface");           
            var repository = Assembly.Load("Practice.Repository");

            //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            //builder.RegisterAssemblyTypes(iServices, services)
            //  .Where(t => t.Name.EndsWith("Services"))
            //  .AsImplementedInterfaces();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)              
              .AsImplementedInterfaces();
        }
    }

}
