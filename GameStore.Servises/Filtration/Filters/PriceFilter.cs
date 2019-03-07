using GameStore.Models.Entities;
using System;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    internal class PriceFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
            GameFilterCriteria model,
            Expression<Func<Game, bool>> existingFilter = null)
        {
           Expression<Func<Game, bool>> priceFilter = game =>
                game.Price > model.MinPrice && game.Price < model.MaxPrice;

            if (existingFilter != null)
            {
                priceFilter = CombineFilters(existingFilter, priceFilter);
            }

            return priceFilter;
        }
    }
}
