using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.Models.Enums;
using GameStore.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Services.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<Order> _orderLogger;

        private readonly ILogger<OrderDetails> _orderDetailsLogger;

        public OrderService(IUnitOfWork unitOfWork,
            ILogger<Order> orderLogger,
            ILogger<OrderDetails> orderDetailsLogger)
        {
            _unitOfWork = unitOfWork;
            _orderLogger = orderLogger;
            _orderDetailsLogger = orderDetailsLogger;
        }

        public Order GetOrderById(string orderId)
        {
            var order = _unitOfWork.GetRepository<Order>().GetSingle(x => x.Id == orderId, i => i.OrderDetails.Select(x => x.Game));
            return order;
        }

        public bool CheckProductInBasket(string customerId, string gameId)
        {
            var order = _unitOfWork.GetRepository<Order>()
                .GetSingle(x => x.CustomerId == customerId && x.Stage == OrderStage.InProgress, i => i.OrderDetails);

            if (order == null)
            {
                return false;
            }

            var exist = order.OrderDetails.Any(x => x.GameId == gameId);

            return exist;
        }

        public void AddProductToBasket(Game game, string customerId)
        {
            var order = _unitOfWork.GetRepository<Order>()
                .GetSingle(x => x.CustomerId == customerId && x.Stage == OrderStage.InProgress, i => i.OrderDetails);

            if (order == null)
            {
                AddOrder(game, customerId);
            }
            else
            {
                UpdateOrder(game, order);
            }
        }


        public IEnumerable<Order> GetAllOrders(OrderFilterCriteria filter = null)
        {
            Expression<Func<Order, bool>> predicate = null;

            if (filter != null)
            {
                predicate = (order) =>
                     order.OrderDate > filter.StartDate && order.OrderDate < filter.EndDate;
            }

            var orders = _unitOfWork.GetRepository<Order>().GetMany(predicate,
                null,
                null,
                null,
                i => i.OrderDetails);

            return orders;
        }

        public Order GetOrderByCustomerId(string customerId)
        {
            var order = _unitOfWork.GetRepository<Order>()
                .GetSingle(x => x.CustomerId == customerId && x.Stage == OrderStage.InProgress,
                    i => i.OrderDetails.Select(x => x.Game));

            return order;
        }

        public void ChangeProductQuantity(string orderDetailsId, short units)
        {
            var orderDetailsRepository = _unitOfWork.GetRepository<OrderDetails>();

            var orderDetails = orderDetailsRepository.GetSingle(x => x.Id == orderDetailsId);

            var oldOrderDetails = Mapper.Map<OrderDetails>(orderDetails);

            var game = _unitOfWork.GetRepository<Game>().GetSingle(x => x.Id == orderDetails.GameId);

            if (game.UnitsInStock < units)
            {
                throw new Exception("The user is trying to add more units of product than are in stock.");
            }

            orderDetails.Quantity = units;
            orderDetails.OrderDetailTotal = game.Price * units;

            orderDetailsRepository.Update(orderDetails);
            _unitOfWork.Save();

            _orderDetailsLogger.WriteUpdateActionLog(oldOrderDetails, orderDetails);
        }

        public void DeleteOrderDetails(string orderDetailsId)
        {
            var orderDetails = _unitOfWork.GetRepository<OrderDetails>().GetSingle(x => x.Id == orderDetailsId);

            CheckForNull(orderDetails, "orderDetails is null");

            _unitOfWork.GetRepository<OrderDetails>().Delete(orderDetails);

            _unitOfWork.Save();
            _orderDetailsLogger.WriteDeleteActionLog(orderDetails);
        }

        private void AddOrder(Game game, string customerId)
        {
            var order = CreateOrder(customerId);

            var orderDetails = CreateOrderDetails(game.Id, game.Price);
            order.OrderDetails.Add(orderDetails);

            _unitOfWork.GetRepository<Order>().Add(order);

            _unitOfWork.Save();

            _orderDetailsLogger.WriteCreateActionLog(orderDetails);
            _orderLogger.WriteCreateActionLog(order);
        }

        private void UpdateOrder(Game game, Order order)
        {
            var orderDetails = order.OrderDetails
                .FirstOrDefault(x => x.GameId == game.Id);

            if (orderDetails != null)
            {
                return;
            }

            orderDetails = CreateOrderDetails(game.Id, game.Price);
            order.OrderDetails.Add(orderDetails);

            _unitOfWork.GetRepository<Order>().Update(order);
            _unitOfWork.Save();
            _orderDetailsLogger.WriteCreateActionLog(orderDetails);
        }

        private Order CreateOrder(string customerId)
        {
            return new Order
            {
                Id = Guid.NewGuid().ToString(),
                OrderDate = DateTime.UtcNow,
                CustomerId = customerId,
                Stage = OrderStage.InProgress,
                OrderDetails = new List<OrderDetails>()
            };
        }

        private OrderDetails CreateOrderDetails(string productId, decimal price)
        {
            return new OrderDetails()
            {
                Id = Guid.NewGuid().ToString(),
                GameId = productId,
                OrderDetailTotal = price,
                Quantity = 1,
            };
        }
    }
}
