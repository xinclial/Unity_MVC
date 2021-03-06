﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {
            //创建属性的集合  
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口  
            Type type = typeof(TResult);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列  
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in value)
            {
                //创建一个DataRow实例  
                DataRow row = dt.NewRow();
                //给row 赋值  
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable  
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
