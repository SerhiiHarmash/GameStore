using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.ViewModel
{
    public class PublisherViewModel
    {
        [Required(ErrorMessage = "Please enter company name")]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please add description")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 100, ErrorMessage = "Description should be minimum 100 characters and a maximum of 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Home page is required")]
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?", ErrorMessage = "Please, input url address")]
        [Display(Name = "Home page")]
        public string HomePage { get; set; }
    }
}