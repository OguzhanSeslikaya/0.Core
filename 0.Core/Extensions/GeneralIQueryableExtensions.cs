using _0.Core.Exceptions;
using System.Linq.Expressions;

namespace _0.Core.Extensions
{
    public static class GeneralIQueryableExtensions
    {
        public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending)
        {
            if (string.IsNullOrEmpty(propertyName))
                return source;

            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, $"Property '{propertyName}' does not exist on type {typeof(T)}");

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
                source.Expression,
                lambda
            );

            var createdDateProperty = Expression.Property(parameter, "CreatedDate");
            var createdDateLambda = Expression.Lambda(createdDateProperty, parameter);

            string thenByMethod = "ThenByDescending";
            var thenByExpression = Expression.Call(
                typeof(Queryable),
                thenByMethod,
                new Type[] { typeof(T), createdDateProperty.Type },
                resultExpression,
                createdDateLambda
            );

            return source.Provider.CreateQuery<T>(thenByExpression);
        }
    }
}
