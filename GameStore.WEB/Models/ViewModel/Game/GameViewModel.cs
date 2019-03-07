using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.ViewModel
{
    public class GameViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [RegularExpression("^[\\w, \\d, \\w]+$", ErrorMessage = "Key must contain only alphabet symbols and white character symbols")]
        [Required(ErrorMessage = "Key is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Key should be minimum 2 characters and a maximum of 100 characters")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be minimum 2 characters and a maximum of 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(0.01, 1000000.00, ErrorMessage = "OrderDetailTotal must be between 0.01 and 1000000.00")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DisplayName("Units in stock")]
        [Required(ErrorMessage = "Specify quantity")]
        [Range(0, short.MaxValue, ErrorMessage = "quantity cannot be negative and must be less 32767")]
        public short UnitsInStock { get; set; }

        [Required(ErrorMessage = "Discontinued is required")]
        public bool Discontinued { get; set; }

        [DisplayName("Release date")]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("View count")]
        public int? ViewCount { get; set; }

        [Required(ErrorMessage = "PublisherCompany is required")]
        public String Publisher { get; set; }

        [Required(ErrorMessage = "Genres is required")]
        public ICollection<string> Genres { get; set; }

        public ICollection<string> Comments { get; set; }

        [Required(ErrorMessage = "Platform of types is required")]
        public ICollection<string> PlatformTypes { get; set; }
    }
}