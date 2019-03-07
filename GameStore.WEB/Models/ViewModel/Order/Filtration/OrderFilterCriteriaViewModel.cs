using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.ViewModel.Order.Filtration
{
    public class OrderFilterCriteriaViewModel
    {
        [DisplayName("Start date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("End date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}