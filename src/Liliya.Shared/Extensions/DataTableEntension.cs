using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// DataTable拓展
    /// </summary>
    public class DataTableEntension
    {

        /// <summary>
        /// 将DataTable数据源转换成实体类
        /// </summary>
        public static List<T> DataTableToModel<T>(DataTable dt) where T : new()
        {
            List<T> ts = new List<T>();// 定义集合
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite) continue;
                        var value = dr[pi.Name];
                        if (value != DBNull.Value)
                        {
                            switch (pi.PropertyType.FullName)
                            {
                                case "System.Decimal":
                                    pi.SetValue(t, decimal.Parse(value.ToString()), null);
                                    break;
                                case "System.String":
                                    pi.SetValue(t, value.ToString(), null);
                                    break;
                                case "System.Int32":
                                    pi.SetValue(t, int.Parse(value.ToString()), null);
                                    break;
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }


        /// <summary>
        /// DataTable转为实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ModelToDataTable<T>(List<T> list) where T : new()
        {
            DataTable dt = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                dt.Columns.Add(prop.Name, t);
            }

            foreach (T item in list)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }

        /// <summary>
        /// 确定指定类型为空
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }


        /// <summary>
        ///  如果type为Nullable则返回基础类型，否则返回类型  
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType && IsNullable(t))
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

    }
}
