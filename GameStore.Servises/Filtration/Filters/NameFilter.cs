using GameStore.Models.Entities;
using System;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    internal class NameFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
            GameFilterCriteria model,
            Expression<Func<Game, bool>> existingFilter = null)
        {
            if (model.GameName == null)
            {
                return existingFilter;
            }

            Expression<Func<Game, bool>> nameFilter = game => game.Name.Contains(model.GameName);

            if (existingFilter != null)
            {
                nameFilter = CombineFilters(existingFilter, nameFilter);
            }

            return nameFilter;
        }
    }
}
