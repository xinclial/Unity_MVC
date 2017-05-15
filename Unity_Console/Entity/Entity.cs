using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_Console.Entity
{
    public interface IBird
    {
        /// <summary>  
        /// 讲话  
        /// </summary>  
        void Say();
    }

    public class Swallow : IBird
    {
        public void Say()
        {
            Console.WriteLine("燕子在叫...");
        }
    }

    public interface IBirdHome
    {
        IBird Swallow { get; set; }
    }

    //public class BirdHome : IBirdHome
    //{
    //    public IBird Swallow { get; set; }

    //    public BirdHome(IBird bird)
    //    {
    //        this.Swallow = bird;
    //    }
    //}


    public class BirdHome : IBirdHome
    {
        /// <summary>  
        /// 属性注入  
        /// </summary>  
        [Dependency]
        public IBird Swallow { get; set; }
    }
}
