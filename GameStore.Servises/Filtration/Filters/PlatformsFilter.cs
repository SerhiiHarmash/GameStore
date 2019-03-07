using GameStore.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    internal class PlatformsFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
           GameFilterCriteria model,
           Expression<Func<Game, bool>> existingFilter = null)
        {
            Expression<Func<Game, bool>> platformsFilter = game =>
                game.PlatformTypes.Any(g => model.PlatformTypes.Contains(g.Type));

            if (existingFilter != null)
            {
                platformsFilter = CombineFilters(existingFilter, platformsFilter);
            }

            return platformsFilter;
        }
    }
}
