using System.Web.Mvc;
using GameStore.Contracts.Interfaces.Services;

namespace GameStore.WEB.Controllers
{
    public class ShipperController : Controller
    {
        private readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        public ActionResult GetAllShippers()
        {
            var shippers = _shipperService.GetAllShippers();

            return View("Shippers", shippers);
        }
    }
}