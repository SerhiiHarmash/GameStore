using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GameStore.Models.Entities
{
    public class Genre : BaseEntity
    {
        [Index(IsUnique = true)]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }

        public string ParentGenreId { get; set; }

        public bool IsDeleted { get; set; }

        public Genre ParentGenre { get; set; }

       // [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
