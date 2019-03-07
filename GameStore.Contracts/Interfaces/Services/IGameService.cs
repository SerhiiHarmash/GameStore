using System.Collections.Generic;
using GameStore.Models.Entities;
using GameStore.Models.Filter;


namespace GameStore.Contracts.Interfaces.Services
{
   public interface IGameService
    {
        void Add(Game game, 
            ICollection<string> genres,
            ICollection<string> platformTypes,
            string publisher);  

        void Delete(string key);

        void Edit(Game game,
            ICollection<string> genres = null,
            ICollection<string> platformTypes = null,
            string publisher = null);

        Game GetGameByKey(string key);

        Game GetGameById(string gameId);

        bool CheckIfGameExists(string key);

        IEnumerable<Game> GetAllGames(GameFilterCriteria gameFilterCriteria = null);

        int GetCountByFilterCriteria(GameFilterCriteria gameFilterCriteria = null);

        IEnumerable<Game> GetGameByGenre(string name);    

        IEnumerable<Game> GetGameByPlatformType(string name);

        int GetGamesCount();

        short GetUnitsInStock(string gameKey);
    }
}
