using AutoMapper;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.Models.Enums;
using GameStore.Models.Filter;
using GameStore.WEB.Models.ViewModel;
using GameStore.WEB.Models.ViewModel.Order;
using GameStore.WEB.Models.ViewModel.Order.Filtration;
using GameStore.WEB.PaymentStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IGameService _gameService;


        public OrderController(
            IOrderService orderService,
            IGameService gameService)
        {
            _orderService = orderService;
            _gameService = gameService;
        }

        public ActionResult GetAllOrders(OrderFilterCriteriaViewModel Filter)
        {
            if (Filter.EndDate == null)
            {
                Filter.EndDate = DateTime.UtcNow;
            }

            if (Filter.StartDate == null)
            {
                Filter.StartDate = DateTime.UtcNow.AddYears(-30);
            }
          
            var model = new OrderShowcaseViewModel();

            var filter = Mapper.Map<OrderFilterCriteriaViewModel, OrderFilterCriteria>(Filter);

            model.Filter = Filter;

            var orders = _orderService.GetAllOrders(filter).ToList();

            model.Ordes = Mapper.Map<List<Order>, List<ShortOrderDetailsViewModel>>(orders);

            return View("Orders", model);
        }

        public ActionResult GetOrder(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);

            var model = Mapper.Map<Order, OrderViewModel>(order);

            return View("Order", model);
        }

        public ActionResult AddToBasket(string gamekey)
        {
            if (!_gameService.CheckIfGameExists(gamekey))
            {
                throw new Exception("the game is not exist");
            }

            var game = _gameService.GetGameByKey(gamekey);

            var customerId = new Guid().ToString();

            var isProductInBasket = _orderService.CheckProductInBasket(customerId, game.Id);

            if (!isProductInBasket)
            {
                _orderService.AddProductToBasket(game, customerId);
            }

            return RedirectToAction("OpenBasket");
        }

        [HttpPost]
        public ActionResult ChangeUnitsProduct(string orderDetailsId, string productKey, short units)
        {
            var unitsInStock = _gameService.GetUnitsInStock(productKey);
            if (unitsInStock >= units)
            {
                _orderService.ChangeProductQuantity(orderDetailsId, units);
            }

            return RedirectToAction("OpenBasket");
        }


        [HttpPost]
        public ActionResult DeleteOrderDetails(string orderDetailsId)
        {
            _orderService.DeleteOrderDetails(orderDetailsId);


            return RedirectToAction("OpenBasket");
        }

        public ActionResult OpenBasket()
        {
            var customerId = new Guid().ToString();

            var basket = _orderService.GetOrderByCustomerId(customerId);                

            if (basket == null || basket.OrderDetails.Count == 0)
            {
                return View("Basket", null);
            }

            var model = Mapper.Map<Order, OrderViewModel>(basket);

            return View("Basket", model);
        }

        public ActionResult MakeOrder()
        {
            var customerId = new Guid().ToString();

            var order = _orderService.GetOrderByCustomerId(customerId);

            var model = Mapper.Map<Order, OrderViewModel>(order);

            return View("NewOrder", model);
        }

        public ActionResult Bay(PaymentType paymentType)
        {
            var customerId = new Guid().ToString();

            var payment = new PaymentResolver().CreatePaymentInstance(paymentType);

            return payment.Pay(customerId);
        }
    }
}