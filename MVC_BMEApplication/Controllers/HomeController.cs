using MVC_BMEApplication.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BMEApplication.Controllers
{
    [ViewActionFilter]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ManageAircraft()
        {
            Models.Aircraft oAircraft = new Models.Aircraft();
            List<Models.Aircraft> lstAircraft = null;
            if (ModelState.IsValid)
            {
                lstAircraft = oAircraft.GetAircraftInformation(0, "ALL");
            }
            ViewBag.AircraftList = lstAircraft;
            return View();
        }
        [HttpGet]
        public ActionResult ViewAircraft(Int64 nAircraftID)
        {
            Models.Aircraft oAircraft = new Models.Aircraft();
            List<Models.Aircraft> lstAircraft = null;
            if (ModelState.IsValid)
            {
                lstAircraft = oAircraft.GetAircraftInformation(nAircraftID, "ONE");
                if (lstAircraft != null && lstAircraft.Count > 0)
                {
                    oAircraft = lstAircraft[0];
                }
            }
            ViewBag.UserID = nAircraftID;
            return View(oAircraft);
        }
        [HttpPost]
        public ActionResult ViewAircraft(Models.Aircraft oAircraft)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ManageAirline()
        {
            Models.Airline oAirline = new Models.Airline();
            List<Models.Airline> lstAirline = null;
            if (ModelState.IsValid)
            {
                lstAirline = oAirline.GetAirlineInformation(0, "ALL");
            }
            ViewBag.AircraftList = lstAirline;
            return View();
        }
        [HttpPost]
        public ActionResult ManageAirline(Models.Airline oAirline)
        {
            return View();
        }

        public ActionResult ViewBMEData()
        {
            return View();
        }

        public ActionResult UploadBMEData()
        {
            return View();
        }

        public ActionResult UnmatchedBMEData()
        {
            return View();
        }

        public ActionResult ViewStandData()
        {
            return View();
        }

        public ActionResult UploadStandData()
        {
            return View();
        }

        public ActionResult UnmatchedStandData()
        {
            return View();
        }
        public ActionResult ViewAssociationData()
        {
            return View();
        }

        public ActionResult UploadAssociationData()
        {
            return View();
        }

        public ActionResult NewAssociationData()
        {
            return View();
        }
    }
}