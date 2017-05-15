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
using System.Text;
using System.Threading.Tasks;
using Unity_Console.Entity;

namespace Unity_Console
{
    class Program
    {

        [Dependency]
        public static IBirdHome service { get; set; }

        static void Main(string[] args)
        {
            IUnityContainer unityContainer = new UnityContainer();
            //实现注入  
            unityContainer.RegisterType<IBird, Swallow>();
            unityContainer.RegisterType<IBirdHome, BirdHome>();

            IBirdHome birdHome = unityContainer.Resolve<IBirdHome>();
            birdHome.Swallow.Say();

            Console.Read();

        }
    }
}
