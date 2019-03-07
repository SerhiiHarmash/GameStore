using GameStore.Models.Entities;
using GameStore.Models.Filter;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtration.Filters
{
    internal class TimeFilter : BaseFilter, IFilter
    {
        public Expression<Func<Game, bool>> AddFilter(
           GameFilterCriteria model,
           Expression<Func<Game, bool>> existingFilter = null)
        {
            var time = DateTime.UtcNow.AddDays(-(double) model.GamePublished);
            var aimTime = new DateTime(time.Year, time.Month, time.Day);
            Expression<Func<Game, bool>> timeFilter = game =>
                game.ReleaseDate > aimTime;

            if (existingFilter != null)
            {
                timeFilter = CombineFilters(existingFilter, timeFilter);
            }

            return timeFilter;
        }
    }
}
