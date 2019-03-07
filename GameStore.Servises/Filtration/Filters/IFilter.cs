using GameStore.Models.Entities;
using System;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration.Filters
{
    public interface IFilter
    {
        Expression<Func<Game, bool>> AddFilter(
            GameFilterCriteria model,
            Expression<Func<Game, bool>> existingFilter = null);
    }
}
