using AutoMapper;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.WEB.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.WEB.Controllers
{
    public class CommentController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;


        public CommentController(IGameService gameService, ICommentService commentService)
        {
            _gameService = gameService;
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult LeaveComment(string gamekey, CommentViewModel NewComment)
        {
            if (!ModelState.IsValid)
            {
                var comments = _commentService.GetCommentsByGame(gamekey);

                var commentsModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
                var commentsView = new CommentsView();
                commentsView.CommentList = commentsModel.ToList();
                commentsView.NewComment = NewComment;
                return View("CommentsByGame", commentsView);
            }

            if (!_gameService.CheckIfGameExists(gamekey))
            {
                ModelState.AddModelError("", "The game is null");
            }

            if (NewComment.ParentCommentId != null)
            {
                if (!_commentService.CheckIfCommentExists(NewComment.ParentCommentId))
                {
                    ModelState.AddModelError("", "Parent comment is not exist");
                }
            }

            var comment = Mapper.Map<CommentViewModel, Comment>(NewComment);

            _commentService.Add(comment, gamekey);

            return RedirectToAction("GetAllCommentsByGame", new { gamekey = gamekey });
        }

        public ActionResult GetAllCommentsByGame(string gamekey)
        {
            var comments = _commentService.GetCommentsByGame(gamekey);

            if (comments == null)
            {
                var message = string.Format($"The comments do not exist for gameKey:{gamekey}");
                var messageResult = Json(message, JsonRequestBehavior.AllowGet);
                return messageResult;
            }

            var commentsModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
            var commentsView = new CommentsView();
            commentsView.CommentList = commentsModel.ToList();

            return View("CommentsByGame", commentsView);
        }

        //[HttpPost]
        //public ActionResult RemoveComment(string gamekey, string commentId)
        //{
        //    if (!_gameService.CheckIfGameExists(gamekey))
        //    {
        //        var message = string.Format($"The game doesn't exist with the gameKey:{gamekey}");
        //        var messageResult = Json(message, JsonRequestBehavior.AllowGet);
        //        return messageResult;
        //    }

        //    if (!_commentService.CheckIfCommentExists(commentId))
        //    {
        //        ModelState.AddModelError("", "CommentId doesn't exist");

        //        var comments = _commentService.GetCommentsByGame(gamekey);
        //        var commentsModel = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
        //        var commentsView = new CommentsView
        //        {
        //            CommentList = commentsModel.ToList()
        //        };
        //        return View("CommentsByGame", commentsView);
        //    }

        //    _commentService.RemoveComment(commentId);

        //    return RedirectToAction("GetAllCommentsByGame", new { gamekey = gamekey });
        //}

        [HttpPost]
        public ActionResult RemoveComment(string gameId, string commentId)
        {           
            var game = _gameService.GetGameById(gameId);
            if (game == null)
            {
                var messageResult = Json("Game doesn't exist", JsonRequestBehavior.AllowGet);
                return messageResult;
            }

            _commentService.RemoveComment(commentId);

            return RedirectToAction("GetAllCommentsByGame", new { gamekey = game.Key });
        }
    }
}