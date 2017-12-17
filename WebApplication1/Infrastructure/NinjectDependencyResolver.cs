

using Ninject;
using Practice.Interface;
using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication1.Infrastructure
{
    /// <summary>
    /// 依赖解析器
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private static IKernel kernel;

        public static IKernel Kernel
        {
            get { return NinjectDependencyResolver.kernel; }
            private set { NinjectDependencyResolver.kernel = value; }
        }

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return kernel.GetAll<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public TService GetInstance<TService>(string key)
        {
            return kernel.TryGet<TService>(key);
        }

        public TService GetInstance<TService>()
        {
            return kernel.TryGet<TService>();
        }

        public object GetInstance(Type serviceType, string key)
        {
            return kernel.TryGet(serviceType, key);
        }

        public object GetInstance(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUser>().To<UserImpl>();           
        }
    }
}