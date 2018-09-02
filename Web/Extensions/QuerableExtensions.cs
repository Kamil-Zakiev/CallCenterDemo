using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Web.Controllers;
using Web.Models.Common;
using Web.Models.Requests;

namespace Web.Extensions
{
    public static class QuerableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, UserLoadParams userLoadParams)
        {
            foreach (var filter in userLoadParams.FilterParams)
            {
                var paramExpr = Expression.Parameter(typeof(T), "dto");
                var propertyExpr = Expression.Property(paramExpr, typeof(T), filter.Key.Substring("filter.".Length));
                var toLowerExpr = Expression.Call(
                    propertyExpr,
                    typeof(string).GetMethod("ToLower", new Type[] { }));

                var valueExpr = Expression.Constant(filter.Value);
                var containsCall = Expression.Call(
                    toLowerExpr,
                    typeof(string).GetMethod("Contains"),
                    valueExpr);

                var expr = Expression.Lambda<Func<T, bool>>(containsCall, paramExpr);
                query = query.Where(expr);
            }

            return query;
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, UserLoadParams userLoadParams)
        {
            var pageSize = PagesInfo.DefaultPageSize;
            var skipCount = (userLoadParams.Page - 1) * pageSize;
            return query
                .Skip(skipCount)
                .Take(pageSize);
        }
    }
}