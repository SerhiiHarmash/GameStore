using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.Models.Enums;
using GameStore.Models.Filter;
using GameStore.Services.Filtration;
using GameStore.Services.Filtration.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Services.Services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Game> _logger;

        public GameService(IUnitOfWork unitOfWork, ILogger<Game> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void Add(Game game,
            ICollection<string> genreList,
            ICollection<string> platformTypeList,
            string publisher)
        {
            CheckForNull(game, "The game is not exist");
            var gameRepository = _unitOfWork.GetRepository<Game>();

            if (gameRepository.Any(x => x.Key == game.Key))
            {
                throw new Exception("The game with such the key already exists");
            }

            game.Id = Guid.NewGuid().ToString();
            game.Genres = AddGenresToGame(genreList);
            game.PlatformTypes = AddPlatformTypesToGame(platformTypeList);
            game.Publisher = AddPublisherToGame(publisher);
            game.ReleaseDate = DateTime.UtcNow;


            gameRepository.Add(game);
            _unitOfWork.Save();
            _logger.WriteCreateActionLog(game);
        }


        public void Edit(
            Game game,
            ICollection<string> genreList = null,
            ICollection<string> platformTypeList = null,
            string publisher = null)
        {
            CheckForNull(game, "The game is not exist");

            var gameRepository = _unitOfWork.GetRepository<Game>();

            var gameFromDb = gameRepository.GetSingle(x => x.Id == game.Id,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes);

            var gameBeforeChange = Mapper.Map<Game>(gameFromDb);
            var updatedGame = Mapper.Map(game, gameFromDb);

            updatedGame.Genres = AddGenresToGame(genreList);
            updatedGame.PlatformTypes = AddPlatformTypesToGame(platformTypeList);
            updatedGame.Publisher = AddPublisherToGame(publisher);
            updatedGame.PublisherId = gameFromDb.Publisher.Id;

            gameRepository.Update(updatedGame);

            _unitOfWork.Save();
            _logger.WriteUpdateActionLog(gameBeforeChange, gameFromDb);
        }

        public void Delete(string key)
        {
            var gameRepository = _unitOfWork.GetRepository<Game>();
            var game = gameRepository.GetSingle(x => x.Key == key,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes);

            CheckForNull(game, "The game is not exist");

            if (game.Comments != null)
            {
                var comments = game.Comments.ToList();
                comments.ForEach(x => _unitOfWork.GetRepository<Comment>().Delete(x));
            }

            gameRepository.Delete(game);

            _unitOfWork.Save();
            _logger.WriteDeleteActionLog(game);
        }

        public bool CheckIfGameExists(string key)
        {
            var exist = _unitOfWork.GetRepository<Game>().Any(x => x.Key == key || x.Id == key);


            return exist;
        }

        public IEnumerable<Game> GetAllGames(GameFilterCriteria gameFilterCriteria)
        {
            var skip = 0;
            var take = int.MaxValue;

            var sorting = new SortingResolver().CreateSorting(gameFilterCriteria);
            var predicate = new GamesFilter().ComposeFilter(gameFilterCriteria);

            if (gameFilterCriteria?.Skip != null && gameFilterCriteria.ItemsPerPage != null)
            {
                skip = (int)gameFilterCriteria.Skip;
                take = (gameFilterCriteria.ItemsPerPage != ItemsPerPage.All) ? (int)gameFilterCriteria.ItemsPerPage : take;
            }

            var allGames = _unitOfWork.GetRepository<Game>().GetMany(predicate, sorting, skip, take,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes).ToList();
            //var allGames = _unitOfWork.GetRepository<Game>().GetMany(predicate, sorting, skip, take,
            //    i => i.Comments
            //   ).ToList();


            AddKeyToGames(allGames);

            return allGames;
        }

        public int GetCountByFilterCriteria(GameFilterCriteria gameFilterCriteria = null)
        {
            Expression<Func<Game, bool>> predicate = null;

            if (gameFilterCriteria != null)
            {
                predicate = new GamesFilter().ComposeFilter(gameFilterCriteria);
            }

            var count = _unitOfWork.GetRepository<Game>().Count(predicate);

            return count;
        }

        public Game GetGameByKey(string key)
        {
            CheckStringIsNullOrWhiteSpace(key, "Error Game of key iss null or write space");
            var gameRepository = _unitOfWork.GetRepository<Game>();

            var game = gameRepository.GetSingle(x => x.Key == key || x.Id == key,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes);

            if (game == null)
            {
                return null;
            }

            _unitOfWork.Save();

            game.ViewCount++;
            gameRepository.Update(game);
            _unitOfWork.Save();

            return game;
        }

        public Game GetGameById(string gameId)
        {
            CheckStringIsNullOrWhiteSpace(gameId, "Error gameid is null or write space");
            var gameRepository = _unitOfWork.GetRepository<Game>();

            var game = gameRepository.GetSingle(x => x.Id == gameId,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes);

            if (game == null)
            {
                return null;
            }

            game.ViewCount++;
            gameRepository.Update(game);
            _unitOfWork.Save();

            return game;
        }

        public IEnumerable<Game> GetGameByGenre(string name)
        {
            CheckStringIsNullOrWhiteSpace(name, "Error Genre of name is null or write space");

            var games = _unitOfWork.GetRepository<Game>().GetMany(x => x.Genres.Select(gn => gn.Name).Contains(name),
                null,
                null,
                null,
                i => i.Publisher,
                i => i.Genres,
                i => i.PlatformTypes);

            return games;
        }

        public IEnumerable<Game> GetGameByPlatformType(string name)
        {
            CheckStringIsNullOrWhiteSpace(name, "Error Genre of name is null or write space");

            var games = _unitOfWork.GetRepository<Game>().GetMany(x => x.Genres.Select(pt => pt.Name).Contains(name),
                null,
                null,
                null,
                i => i.Publisher,
                i => i.Genres,
                i => i.PlatformTypes);

            return games;
        }

        public int GetGamesCount()
        {
            var count = _unitOfWork.GetRepository<Game>().Count();

            return count;
        }

        public short GetUnitsInStock(string gameKey)
        {
            var units = _unitOfWork.GetRepository<Game>().GetSingle(x => x.Key == gameKey).UnitsInStock;

            return units;
        }


        private ICollection<PlatformType> AddPlatformTypesToGame(ICollection<string> platformTypeList)
        {
            if (platformTypeList == null || platformTypeList.Count == 0)
            {
                return null;
            }

            var platformTypes = _unitOfWork.GetRepository<PlatformType>()
                .GetMany(x => platformTypeList.Contains(x.Type), null, null, null, i => i.Games)
                .ToList();

            return platformTypes;
        }

        private ICollection<Genre> AddGenresToGame(ICollection<string> genreList)
        {
            if (genreList == null || genreList.Count == 0)
            {
                return null;
            }

            var genres = _unitOfWork.GetRepository<Genre>().GetMany(x => genreList.Contains(x.Name), null, null, null, i => i.ParentGenre, i => i.Games).ToList();

            if (genreList.Count != genres.Count)
            {
                throw new Exception("The game contains a non-existent genre");
            }

            return genres;
        }

        private Publisher AddPublisherToGame(string publisherName)
        {
            if (publisherName == null)
            {
                return null;
            }

            var publisher = _unitOfWork.GetRepository<Publisher>().GetSingle(x => x.CompanyName == publisherName, i => i.Games);

            return publisher;
        }

        private void AddKeyToGames(IEnumerable<Game> allGames)
        {
            foreach (var game in allGames)
            {
                if (game.Key == null)
                {
                    game.Key = game.Id;
                }
            }
        }
    }
}
