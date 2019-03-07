using System;
using Newtonsoft.Json;

namespace GameStore.Models.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string Body { get; set; }

        public string ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public string GameId { get; set; }
         

        //[JsonIgnore]
        public Game Game { get; set; }

        public bool IsQuote { get; set; }
    }
}
