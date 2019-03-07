using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models.Entities
{
    public class Game : BaseEntity
    {
        [NotMapped]
        public int ProductId { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100, MinimumLength = 2)]
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool Discontinued { get; set; }

        [NotMapped]
        public int SupplierId { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }

        public string PublisherId { get; set; }

        public int ViewCount { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PlatformType> PlatformTypes { get; set; }      
    }
}
