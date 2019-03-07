using GameStore.Contracts.Interfaces.Services;
using GameStore.WEB.Models.ViewModel.Ban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Models.Enums;

namespace GameStore.WEB.Controllers
{
    public class BanController : Controller
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }

        public ActionResult Ban(string userId)
        {
            var model = new BanViewModel();

            var banDurations = InitializeBanDurations();

            model.BanDurations = new SelectList(banDurations, "Value", "Text");
            model.UserId = userId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Ban(string userId, BanDuration banDuration)
        {
            _banService.Add(userId, banDuration);
            return View("BanResult");
        }

        private List<SelectListItem> InitializeBanDurations()
        {
            var banDurations = new List<SelectListItem>();

            banDurations = ((BanDuration[]) Enum.GetValues(typeof(BanDuration)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToString((int) c),
                    Text = c.ToString()
                })
                .ToList();


            return banDurations;
        }
    }
}