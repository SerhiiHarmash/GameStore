using GameStore.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    internal class GenresFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
           GameFilterCriteria model,
           Expression<Func<Game, bool>> existingFilter = null)
        {
            Expression<Func<Game, bool>> genresFilter = (game) =>
            game.Genres.Any(g => model.Genres.Contains(g.Name));

            if (existingFilter != null)
            {
                genresFilter = CombineFilters(existingFilter, genresFilter);
            }

            return genresFilter;
        }
    }
}
