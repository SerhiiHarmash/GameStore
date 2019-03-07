using GameStore.Models.Entities;
using System.Collections.Generic;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface ICommentService
    {
        void Add(Comment comment, string gameKey);

        IEnumerable<Comment> GetCommentsByGame(string gameKey);

        bool CheckIfCommentExists(string commentId);

        void RemoveComment(string commentId);
    }
}
