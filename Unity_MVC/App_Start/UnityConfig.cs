using Microsoft.Practices.Unity;
using Repository;
using Repository.ImplRepositories;
using Repository.IRepositories;
using Service.ImplServices;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity_MVC.UnityIoc;

namespace Unity_MVC.App_Start
{
    public static class UnityConfig
    {
        public static IUnityContainer unityContainer;
        public static void ServiceUnityConfigRegister(IUnityContainer container)
        {
            unityContainer = container;

            unityContainer.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            unityContainer.RegisterType<I_t_Case_Main_Repositoy, t_Case_Main_Repositoy>();
            unityContainer.RegisterType<I_T_CaseClient_Repository, T_CaseClient_Repository>();

        }

    }
}