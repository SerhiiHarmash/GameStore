using GameStore.Models.Entities;
using System.ComponentModel.DataAnnotations;


namespace GameStore.WEB.Models.ViewModel
{
    public class CommentViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public bool IsQuote { get; set; }

        public bool IsDeleted { get; set; }

        public string GameId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be minimum 3 characters and a maximum of 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Body is required")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Comment should be minimum 1 characters and a maximum of 1000 characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}