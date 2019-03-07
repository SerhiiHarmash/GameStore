using AutoMapper;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.Models.Filter;
using GameStore.WEB.App_LocalResources;
using GameStore.WEB.Models.ViewModel;
using GameStore.WEB.Models.ViewModel.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Models.Enums;

namespace GameStore.WEB.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;


        public GameController(
            IGameService gameService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateGame()
        {
            var model = new EditingGameViewModel();
            model.Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList();
            model.PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(x => x.Type).ToList();
            model.Publishers = _publisherService.GetAllPublisher().Select(x => x.CompanyName).ToList();
            return View("CreateGame", model);
        }

        [HttpPost]
        public ActionResult CreateGame(EditingGameViewModel model)
        {
            model.Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList();
            model.PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(x => x.Type).ToList();
            model.Publishers = _publisherService.GetAllPublisher().Select(x => x.CompanyName).ToList();

            if (!ModelState.IsValid)
            {
                return View("CreateGame", model);
            }

            if (_gameService.CheckIfGameExists(model.Game.Key))
            {
                ModelState.AddModelError("", "The game with such the key already exists");
                return View("CreateGame", model);
            }

            var game = Mapper.Map<GameViewModel, Game>(model.Game);
            _gameService.Add(game, model.Game.Genres, model.Game.PlatformTypes, model.Game.Publisher);

            return RedirectToAction("CreateGame");
        }

        public ActionResult UpdateGame(string gamekey)
        {
            var model = new EditingGameViewModel();
            var game = _gameService.GetGameByKey(gamekey);
            var gameModel = Mapper.Map<Game, GameViewModel>(game);

            model.Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList();
            model.PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(x => x.Type).ToList();
            model.Publishers = _publisherService.GetAllPublisher().Select(x => x.CompanyName).ToList();
            model.Game = gameModel;

            return View("EditGame", model);
        }

        [HttpPost]
        public ActionResult UpdateGame(EditingGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = Mapper.Map<GameViewModel, Game>(model.Game);
                _gameService.Edit(game, model.Game.Genres, model.Game.PlatformTypes, model.Game.Publisher);

                return RedirectToAction("GetGameByKey", new { key = game.Key });
            }

            model.Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList();
            model.PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(x => x.Type).ToList();
            model.Publishers = _publisherService.GetAllPublisher().Select(x => x.CompanyName).ToList();

            return View("EditGame", model);
        }

        public ActionResult GetGameByKey(string key)
        {
            var game = _gameService.GetGameByKey(key);
            
            if (game == null)
            {
                var message = string.Format($"The game with gameKey:{key} is 't exist");
                var messageResult = Json(message, JsonRequestBehavior.AllowGet);
                return messageResult;
            }

            var gameModel = Mapper.Map<Game, GameViewModel>(game);

            return View("Game", gameModel);
        }

        public ActionResult GetAllGames(GameFilterCriteria gameFilterCriteria)
        {
            IgnoreIfNameIsWhiteSpace(gameFilterCriteria);

            var gamesShowcase = new GamesShowcaseViewModel
            {
                Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList(),
                PlatformTypes = _platformTypeService.GetAllPlatformTypes().Select(x => x.Type).ToList(),
                Publishers = _publisherService.GetAllPublisher().Select(x => x.CompanyName).ToList(),
                ItemsPerPageVariants = CreateItemsPerPageVariants()
            };

            if (!ModelState.IsValid)
            {
                return View("AllGames", gamesShowcase);
            }

            InitializeBaseFilterCriteria(gameFilterCriteria);

            var allGames = _gameService.GetAllGames(gameFilterCriteria);

            var allGamesConverted = Mapper.Map<IEnumerable<Game>, IEnumerable<GameCardViewModel>>(allGames).ToList();

            gamesShowcase.GameCount = _gameService.GetCountByFilterCriteria(gameFilterCriteria);
            gamesShowcase.Games = allGamesConverted;
            gamesShowcase.GameFilterCriteria = gameFilterCriteria;

            return View("AllGames", gamesShowcase);
        }

        [HttpPost]
        public ActionResult RemoveGame(string key)
        {
            _gameService.Delete(key);
            var messageResult = Json("The game is successfully removed ", JsonRequestBehavior.AllowGet);
            return messageResult;
        }

        public ActionResult DownloadGame(string gamekey)
        {
            string path = Server.MapPath("~/FilesBinary/file.txt");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = ".txt";
            string file_name = "file.txt";
            return File(mas, file_type, file_name);
        }

        [OutputCache(Duration = 60)]
        public ActionResult GetGameCount()
        {
            var count = _gameService.GetGamesCount();
            return PartialView("GameCount", count);
        }

        private SelectList CreateItemsPerPageVariants()
        {
            var list = ((ItemsPerPage[])Enum.GetValues(typeof(ItemsPerPage)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToString((int)c),
                    Text = Resource.ResourceManager.GetString(c.ToString())
                })
                .ToList();

            var itemsPerPageVariants = new SelectList(list, "Value", "Text");

            return itemsPerPageVariants;
        }

        private void InitializeBaseFilterCriteria(GameFilterCriteria gameFilterCriteria)
        {
            if (gameFilterCriteria != null && (gameFilterCriteria.ItemsPerPage == null
                                           && gameFilterCriteria.Skip == null
                                           && gameFilterCriteria.SortCriterion == null))
            {
                gameFilterCriteria.SortCriterion = SortFilter.Popular;
                gameFilterCriteria.Skip = 0;
                gameFilterCriteria.ItemsPerPage = ItemsPerPage.Ten;
            }
        }

        private void IgnoreIfNameIsWhiteSpace(GameFilterCriteria gameFilterCriteria)
        {
            if (gameFilterCriteria?.GameName == null)
            {
                return;
            }

            if (gameFilterCriteria.GameName.Trim() == string.Empty)
            {
                gameFilterCriteria.GameName = null;
            }
        }
    }
}