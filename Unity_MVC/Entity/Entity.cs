using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unity_MVC.Entity
{
    public interface IProduct
    {
        string ClassName { get; set; }
        void ShowInfo();
    }
    /// <summary>
    /// 牛奶
    /// </summary>
    public class Milk : IProduct
    {
        public string ClassName { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine("牛奶：{0}", ClassName);
        }
    }
    /// <summary>
    /// 糖
    /// </summary>
    public class Sugar : IProduct
    {
        public string ClassName { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine("糖：{0}", ClassName);
        }
    }
}