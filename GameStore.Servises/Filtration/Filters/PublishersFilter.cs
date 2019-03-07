using GameStore.Models.Entities;
using System;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    internal class PublishersFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
            GameFilterCriteria model,
            Expression<Func<Game, bool>> existingFilter = null)
        {
            Expression<Func<Game, bool>> publishersFilter = game =>
                model.Publishers.Contains(game.Publisher.CompanyName);

            if (existingFilter != null)
            {
                publishersFilter = CombineFilters(existingFilter, publishersFilter);
            }

            return publishersFilter;
        }
    }
}
