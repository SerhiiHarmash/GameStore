using AutoMapper;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.WEB.Models.ViewModel;
using System;
using System.Web.Mvc;

namespace GameStore.WEB.PaymentStrategy
{
    internal class Terminal : IPayment
    {
        private readonly IOrderService _orderService;

        public Terminal()
        {
            _orderService = DependencyResolver.Current.GetService<IOrderService>();
        }

        public ActionResult Pay(string customerId)
        {
            var order = _orderService.GetOrderByCustomerId(customerId);
            var model = Mapper.Map<Order, TerminalViewModel>(order);

            return new ViewResult()
            {
                ViewName = "~/Views/Order/Terminal.cshtml",
                ViewData = new ViewDataDictionary()
                {
                    Model = model
                }
            };
        }
    }
}