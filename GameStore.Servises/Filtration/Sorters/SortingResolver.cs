using GameStore.Models.Entities;
using GameStore.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Models.Enums;

namespace GameStore.Services.Filtration.Sorters
{
    public class SortingResolver
    {
        private readonly Dictionary<SortFilter?,
            Func<IQueryable<Game>, IOrderedQueryable<Game>>> _sorts;

        public SortingResolver()
        {
            _sorts = new Dictionary<SortFilter?, Func<IQueryable<Game>, IOrderedQueryable<Game>>>
            {
                {SortFilter.Popular, games => games.OrderByDescending(x => x.ViewCount)},
                {SortFilter.Commented, games => games.OrderByDescending(x => x.Comments.Count)},
                {SortFilter.New, games => games.OrderByDescending(x => x.ReleaseDate)},
                {SortFilter.PriceAscending, games => games.OrderBy(x => x.Price)},
                {SortFilter.PriceDescending, games => games.OrderByDescending(x => x.Price)}
            };
        }

        public Func<IQueryable<Game>, IOrderedQueryable<Game>> CreateSorting(GameFilterCriteria model)
        {
            return model.SortCriterion != null ? _sorts[model.SortCriterion] : null;
        }
    }

}
