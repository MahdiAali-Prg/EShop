using System;
using System.Linq;
using System.Linq.Expressions;

namespace EShop.Data.Repositories.GenericRepository.GenericExtensionMethods
{
    public static class EfExtensionMethods
    {
        public static IQueryable<TModel> Include<TModel>(this IQueryable<TModel> data, params Expression<Func<TModel, object>>[] includeExpressions)
        {
            if (includeExpressions is null)
            {
                throw new ArgumentNullException();
            }

            foreach (Expression<Func<TModel, object>> expression in includeExpressions)
            {
                data = data.Include(expression);
            }

            return data;
        }

        public static IQueryable<TModel> Where<TModel>(this IQueryable<TModel> data, Expression<Func<TModel, bool>> whereExpression)
        {
            if (whereExpression is null)
            {
                throw new ArgumentNullException();
            }

            data = data.Where(whereExpression);

            return data;
        }
    }
}
