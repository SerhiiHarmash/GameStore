using GameStore.Models.Entities;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtration
{
    internal class BaseFilter
    {
        protected Expression<Func<Game, bool>> CombineFilters(
            Expression<Func<Game, bool>> existingFilter,
            Expression<Func<Game, bool>> newFilter)
        {
            var parameter = Expression.Parameter(typeof(Game));
            var leftVisitor = new ExpressionMergingVisitor(existingFilter.Parameters[0], parameter);
            var left = leftVisitor.Visit(existingFilter.Body);

            var rightVisitor = new ExpressionMergingVisitor(newFilter.Parameters[0], parameter);
            var right = rightVisitor.Visit(newFilter.Body);

            var combinedFilter = Expression.Lambda<Func<Game, bool>>(Expression.AndAlso(left, right), parameter);

            return combinedFilter;
        }
    }
}