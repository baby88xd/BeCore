using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Reflection;

namespace System.Linq.Expressions
{
    public static class DynamicQueryableLikeHelper
    {


        public static IQueryable<T> Where<T>(this IQueryable<T> source, IEnumerable<UosoConditions> conditions) where T : class
        {
            var parser = new UosoExpressionParser<T>();
            var filter = parser.ParserConditions(conditions);
            return source.Where(filter);
        }
        public static IQueryable<T> Pager<T>(this IQueryable<T> query, int pageindex, int pagesize, out int itemCount)
        {
            itemCount = query.Count();
            return query.Skip((pageindex - 1) * pagesize).Take(pagesize);
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="Field"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string Field, string orderby = "orderb")
        {
            if (!string.IsNullOrEmpty(Field))
            {
                ParameterExpression p = Expression.Parameter(typeof(T));
                Expression key = Expression.Property(p, Field);

                var propInfo = GetPropertyInfo(typeof(T), Field);
                var expr = GetOrderExpression(typeof(T), propInfo);

                if (!("desc" == orderby))
                {
                    var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
                    var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                    return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
                }
                else
                {
                    var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
                    var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                    return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
                }
            }
            return query;
        }
        /// <summary>
        /// 获取反射
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        /// <summary>
        /// 获取生成表达式
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

    }




}
