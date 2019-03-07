using GameStore.WEB.Models.ViewModel.Order.Filtration;
using System.Collections.Generic;

namespace GameStore.WEB.Models.ViewModel.Order
{
    public class OrderShowcaseViewModel
    {
        public ICollection<ShortOrderDetailsViewModel> Ordes { get; set; }

        public OrderFilterCriteriaViewModel Filter { get; set; }
    }
}