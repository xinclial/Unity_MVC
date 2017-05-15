using Microsoft.Practices.Unity;
using Repository;
using Repository.ImplRepositories;
using Repository.IRepositories;
using Service;
using Service.ImplServices;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity_MVC.App_Start;
using Unity_MVC.Entity;
using Unity_MVC.UnityIoc;

namespace Unity_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            unityContainer.RegisterType<I_t_Case_Main_Service, t_Case_Main_Service>();
            unityContainer.RegisterType<I_T_CaseClient_Service, T_CaseClient_Service>();

            unityContainer.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            unityContainer.RegisterType<I_t_Case_Main_Repositoy, t_Case_Main_Repositoy>();
            unityContainer.RegisterType<I_T_CaseClient_Repository, T_CaseClient_Repository>();

            //UnityConfig.ServiceUnityConfigRegister(unityContainer);

            UnityControllerFactory controllerFactory = new UnityControllerFactory(unityContainer);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new UnityHttpControllerActivator(unityContainer));


            

        }
    }
}
