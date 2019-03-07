using AutoMapper;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using GameStore.WEB.Models.ViewModel;
using System.Web.Mvc;

namespace GameStore.WEB.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public ActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPublisher(PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(publisherViewModel);
            }

            if (_publisherService.CheckIfPublisherExists(publisherViewModel.CompanyName))
            {
                ModelState.AddModelError("", $"company {publisherViewModel.CompanyName} already exists");
                return View(publisherViewModel);
            }

            var publisher = Mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);

            _publisherService.AddPublisher(publisher);

          
            return RedirectToAction("AddPublisher");
        }

        public ActionResult EditPublisher(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompanyName(companyName);

            var model = Mapper.Map<Publisher, PublisherViewModel>(publisher);
           
            return View("AddPublisher", publisher);
        }

        [HttpPost]
        public ActionResult EditPublisher(PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(publisherViewModel);
            }

            if (_publisherService.CheckIfPublisherExists(publisherViewModel.CompanyName))
            {
                ModelState.AddModelError("", $"company {publisherViewModel.CompanyName} already exists");
                return View(publisherViewModel);
            }

            var publisher = Mapper.Map<PublisherViewModel, Publisher>(publisherViewModel);

            _publisherService.AddPublisher(publisher);


            return RedirectToAction("AddPublisher");
        }


        public ActionResult GetPublisherByCompanyName(string CompanyName)
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                throw new System.Exception("companyName is null or write space");
            }

            var publisher = _publisherService.GetPublisherByCompanyName(CompanyName);
            if (publisher == null)
            {
                var message = $"The pubilsher with CompanyName:{CompanyName} is't exist";
                var messageResult = Json(message, JsonRequestBehavior.AllowGet);
                return messageResult;
            }

            var publisherModel = Mapper.Map<Publisher, PublisherViewModel>(publisher);

            return View("Publisher", publisherModel);
        }
    }
}