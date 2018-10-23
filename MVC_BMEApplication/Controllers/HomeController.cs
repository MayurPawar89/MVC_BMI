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
            string sTask = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    int nStatus;
                    if (oAircraft.AircraftTypeID == 0)
                    {
                        nStatus = oAircraft.InsertUpdateDeleteAircraft(oAircraft, "NEW");
                    }
                    else
                    {
                        nStatus = oAircraft.InsertUpdateDeleteAircraft(oAircraft, "EDIT");
                    }
                    switch (nStatus)
                    {
                        case -1: ViewBag.Message = "Aircraft information already exists";
                            break;
                        case 0: ViewBag.Message = "Aircraft information registerd successfully";
                            break;
                        case 1: ViewBag.Message = "Error";
                            break;
                        case 2: ViewBag.Message = "Aircraft information updated successfully";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return View();
        }

        
        public ActionResult Delete(Int64 nID,string sType,string sActionName,string sControllerName)
        {
            string Message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    int nStatus = 0;
                    if (nID != 0)
                    {
                        switch (sType)
                        {
                            case "User":
                                {
                                    Models.User oUser = new Models.User();
                                    nStatus = oUser.DeleteUser(nID, "DELETE");
                                    if (oUser != null)
                                    {
                                        oUser.Dispose();
                                        oUser = null;
                                    }
                                    break;
                                }
                            case "Aircraft":
                                {
                                    Models.Aircraft oAircraft = new Models.Aircraft();
                                    nStatus = oAircraft.DeleteAircraft(nID, "DELETE");
                                    if (oAircraft != null)
                                    {
                                        oAircraft.Dispose();
                                        oAircraft = null;
                                    }
                                    break;
                                }
                            case "Airline":
                                {
                                    Models.Airline oAirline = new Models.Airline();
                                    nStatus = oAirline.DeleteAirline(nID, "DELETE");
                                    if (oAirline != null)
                                    {
                                        oAirline.Dispose();
                                        oAirline = null;
                                    }
                                    break;
                                }
                            case "BME": { break; }
                            case "Stand": { break; }
                            case "Assocciation": { break; }
                        }
                    }
                    switch (nStatus)
                    {
                        case -1: ViewBag.Message = sType + " information already exists";
                            break;
                        case 1: ViewBag.Message = "Error";
                            break;
                        case 3: ViewBag.Message = sType + " information deleted successfully";
                            break;
                    }
                }
                ViewBag.Action = sActionName;
                ViewBag.ControllerName = sControllerName;
            }
            catch (Exception ex)
            {
            }
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
            ViewBag.AirlineList = lstAirline;
            return View();
        }
        [HttpGet]
        public ActionResult ViewAirline(Int64 nAirlineID)
        {
            Models.Airline oAirline = new Models.Airline();
            List<Models.Airline> lstAirline = null;
            if (ModelState.IsValid)
            {
                lstAirline = oAirline.GetAirlineInformation(nAirlineID, "ONE");
                if (lstAirline != null && lstAirline.Count > 0)
                {
                    oAirline = lstAirline[0];
                }
            }
            ViewBag.AirlineID = nAirlineID;
            return View(oAirline);
        }
        [HttpPost]
        public ActionResult ViewAirline(Models.Airline oAirline)
        {
            string sTask = string.Empty;
            if (ModelState.IsValid)
            {
                int nStatus;
                if (oAirline.AIRLINE_CODE_ID == 0)
                {
                    nStatus = oAirline.InsertUpdateDeleteAirline(oAirline, "NEW");
                }
                else
                {
                    nStatus = oAirline.InsertUpdateDeleteAirline(oAirline, "EDIT");
                }
                switch (nStatus)
                {
                    case -1: ViewBag.Message = "Airline information already exists";
                        break;
                    case 0: ViewBag.Message = "Airline information registerd successfully";
                        break;
                    case 1: ViewBag.Message = "Error";
                        break;
                    case 2: ViewBag.Message = "Airline information updated successfully";
                        break;
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult ManageAirline(Models.Airline oAirline)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewBMEData()
        {
            Models.DataFetch oData = new Models.DataFetch();
            List<Models.BMEData> lstBMEData = new List<Models.BMEData>();
            if (ModelState.IsValid)
            {
                if (oData.CallingView!=null && oData.Task!=null)
                {
                    lstBMEData = oData.RetriveBMEData();
                }
                oData.FromDate = DateTime.Now.Date;
                oData.ToDate = DateTime.Now.Date;
                oData.CallingView = "ViewBME";
                oData.Task = "ALL";
            }
            ViewBag.BMEDataList = lstBMEData;
            return View(oData);
        }
        [HttpPost]
        public ActionResult ViewBMEData(Models.DataFetch oData)
        {
            List<Models.BMEData> lstBMEData = null;
            if (ModelState.IsValid)
            {
               lstBMEData= oData.RetriveBMEData();
            }
            ViewBag.BMEDataList = lstBMEData;
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
        [HttpGet]
        public ActionResult ViewStandData()
        {
            Models.DataFetch oData = new Models.DataFetch();
            List<Models.StandData> lstStandData = new List<Models.StandData>();
            if (ModelState.IsValid)
            {
                if (oData.CallingView != null && oData.Task != null)
                {
                    lstStandData = oData.RetriveStandData();
                }
                oData.FromDate = DateTime.Now.Date;
                oData.ToDate = DateTime.Now.Date;
                oData.CallingView = "ViewStand";
                oData.Task = "ALL";
            }
            ViewBag.StandDataList = lstStandData;
            return View(oData);
        }
        [HttpPost]
        public ActionResult ViewStandData(Models.DataFetch oData)
        {
            List<Models.StandData> lstStandData = null;
            if (ModelState.IsValid)
            {
                lstStandData = oData.RetriveStandData();
            }
            ViewBag.StandDataList = lstStandData;
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
        [HttpGet]
        public ActionResult ViewAssociationData()
        {
            Models.DataFetch oData = new Models.DataFetch();
            List<Models.AssociationData> lstAssociationData = new List<Models.AssociationData>();
            if (ModelState.IsValid)
            {
                if (oData.CallingView != null && oData.Task != null)
                {
                    lstAssociationData = oData.RetriveAssociationData();
                }
                oData.FromDate = DateTime.Now.Date;
                oData.ToDate = DateTime.Now.Date;
                oData.CallingView = "ViewAssociation";
                oData.Task = "ALL";
            }
            ViewBag.AssociationDataList = lstAssociationData;
            return View(oData);
        }
        [HttpPost]
        public ActionResult ViewAssociationData(Models.DataFetch oData)
        {
            List<Models.AssociationData> lstAssociationData = null;
            if (ModelState.IsValid)
            {
                lstAssociationData = oData.RetriveAssociationData();
            }
            ViewBag.AssociationDataList = lstAssociationData;
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