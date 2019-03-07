using System.Collections.Generic;

namespace GameStore.WEB.Models.ViewModel
{
    public class CommentsView
    {
        public CommentViewModel NewComment { get; set; }

        public ICollection<CommentViewModel> CommentList { get; set; }
    }
}