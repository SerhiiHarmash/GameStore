using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using System;
using System.Collections.Generic;

namespace GameStore.Services.Services
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Comment> _logger;

        public CommentService(IUnitOfWork unitOfWork, ILogger<Comment> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void Add(Comment comment, string gameKey)
        {
            var game = _unitOfWork.GetRepository<Game>().GetSingle(x => x.Key == gameKey,
                i => i.Publisher,
                i => i.Comments,
                i => i.Genres,
                i => i.PlatformTypes);

            CheckForNull(game, "The game is not exist");
            CheckForNull(comment, "The comment is not exist");

            comment.Game = game;
            comment.Id = Guid.NewGuid().ToString();
            comment.UserId = new Guid().ToString();
            comment.Date = DateTime.UtcNow;
            _unitOfWork.GetRepository<Comment>().Add(comment);
            _unitOfWork.Save();
            _logger.WriteCreateActionLog(comment);
        }

        public IEnumerable<Comment> GetCommentsByGame(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var comments = _unitOfWork.GetRepository<Comment>().GetMany(x => x.Game.Key == key,
                    null,
                    null,
                    null,
                    i => i.Game);
                return comments;
            }

            throw new Exception("Error gameKey is null or write space");
        }

        public bool CheckIfCommentExists(string commentId)
        {
            var exist = _unitOfWork.GetRepository<Comment>().Any(x => x.Id == commentId);
            return exist;
        }

        public void RemoveComment(string commentId)
        {
            var comment = _unitOfWork.GetRepository<Comment>().GetSingle(x => x.Id == commentId, i => i.Game);
            CheckForNull(comment, "The comment is null");
            comment.IsDeleted = true;
            _unitOfWork.GetRepository<Comment>().Update(comment);
            _unitOfWork.Save();
            _logger.WriteDeleteActionLog(comment);
        }
    }
}
