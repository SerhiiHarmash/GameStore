using System.Collections.Generic;
using GameStore.Models.Entities;
using GameStore.Models.Filter;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface IOrderService
    {
        void AddProductToBasket(Game game, string customerId);

        IEnumerable<Order> GetAllOrders(OrderFilterCriteria filter);

        Order GetOrderByCustomerId(string customerId);

        Order GetOrderById(string orderId);

        bool CheckProductInBasket(string customerId, string gameId);

        void DeleteOrderDetails(string orderDetailsId);

        void ChangeProductQuantity(string orderDetailsId, short units);
    }
}
