[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AngularWebAPI.WEBAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AngularWebAPI.WEBAPI.App_Start.NinjectWebCommon), "Stop")]

namespace AngularWebAPI.WEBAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using DataAccess.DataAccess;
    using Abstractions.Interface;
    using System.Web.Http;
    using WebApiContrib.IoC.Ninject;
    using Abstractions.Configuration;
    using Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            if (EnvironmentConfiguration.Instance.UsesMockData)
            {
                kernel.Bind<IEmployeeRepository>().To<AngularWebAPI.Mock.EFRepository.EmployeeRepository>();
                kernel.Bind<IEmployeeDependantRepository>().To<AngularWebAPI.Mock.EFRepository.EmployeeDependantRepository>();
                kernel.Bind<IEmployeeImageRepository>().To<AngularWebAPI.Mock.EFRepository.EmployeeImageRepository>();

            }
            else
            {
                kernel.Bind<DbContext>().To<AngularWebAPIDataContext>();
                kernel.Bind<IEmployeeRepository>().To<DataAccess.EFRepository.EmployeeRepository>();
                kernel.Bind<IEmployeeDependantRepository>().To<DataAccess.EFRepository.EmployeeDependantRepository>();
                kernel.Bind<IEmployeeImageRepository>().To<DataAccess.EFRepository.EmployeeImageRepository>();
                kernel.Bind<IAppUserService>().To<DefaultAppUserService>();
            }


        }        
    }
}
