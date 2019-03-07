using GameStore.Models.Entities;
using GameStore.Models.Filter;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtration.Filters
{
    internal class MinPriceFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
            GameFilterCriteria model,
            Expression<Func<Game, bool>> existingFilter = null)
        {
            Expression<Func<Game, bool>> priceFilter = game => game.Price > model.MinPrice;

            if (existingFilter != null)
            {
                priceFilter = CombineFilters(existingFilter, priceFilter);
            }

            return priceFilter;
        }
    }
}
