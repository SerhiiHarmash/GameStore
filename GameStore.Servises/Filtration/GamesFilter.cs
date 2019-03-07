using GameStore.Models.Entities;
using GameStore.Services.Filtration.Filters;
using System;
using System.Linq.Expressions;
using GameStore.Models.Filter;

namespace GameStore.Services.Filtration
{
    public class GamesFilter
    {
        public Expression<Func<Game, bool>> ComposeFilter(GameFilterCriteria gameFilterCriteria)
        {
            Expression<Func<Game, bool>> expression = null;

            if (gameFilterCriteria.GameName != null)
            {
                expression = new NameFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.Genres != null && gameFilterCriteria.Genres.Count != 0)
            {
                expression = new GenresFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.Publishers != null && gameFilterCriteria.Publishers.Count != 0)
            {
                expression = new PublishersFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.PlatformTypes != null && gameFilterCriteria.PlatformTypes.Count != 0)
            {
                expression = new PlatformsFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.MinPrice != null)
            {
                expression = new MinPriceFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.MaxPrice != null)
            {
                expression = new MaxPriceFilter().AddFilter(gameFilterCriteria, expression);
            }         

            if (gameFilterCriteria.GamePublished != null)
            {
                expression = new TimeFilter().AddFilter(gameFilterCriteria, expression);
            }

            return expression;
        }
    }
}
