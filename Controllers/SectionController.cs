using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ID_Request.Models;
using ID_Request.Repository;


namespace ID_Request.Controllers
{
    public class SectionController : Controller
    {
        private Data _dataRepository = new Data();
        //new comment
        // GET: Section
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RequestData details)
        {
            _dataRepository.SaveRequestData(details);
            ModelState.Clear();
            return View();
        }

        // Add these methods to handle AJAX requests

        [HttpGet]
        public JsonResult GetAllRequestData()
        {
            var requestList = _dataRepository.GetAllRequestData();
            return Json(requestList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRequestDataById(int id)
        {
            object request = _dataRepository.GetRequestDataById(id);
            return Json(request, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRequestData(RequestData requestData)
        {
            bool result = _dataRepository.SaveRequestData(requestData);
            return Json(new { success = result });
        }

        [HttpPost]
        public JsonResult DeleteRequest(int id)
        {
            bool result = _dataRepository.DeleteRequest(id);
            return Json(new { success = result });
        }
    }
}