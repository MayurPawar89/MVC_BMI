using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_BMEApplication.ActionFilters;

namespace MVC_BMEApplication.Controllers
{
    [ViewActionFilter]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User oUser)
        {
            if (ModelState.IsValid)
            {
                if (oUser.IsValidUser(oUser.UserName, oUser.Password, out oUser))
                {
                    Session["UserName"] = oUser.UserName;
                    Session["UserFullName"] = oUser.FirstName + " " + oUser.LastName;
                    Session["RoleID"] = oUser.Role_ID;
                    Session["RoleName"] = Convert.ToString(oUser.Role_Name);
                    Session["UserID"] = oUser.User_ID;
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User Name or Password is incorrect!");
                }
            }
            return View(oUser);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (ModelState.IsValid)
            {
                Models.User oUser = new Models.User();
                List<Models.User> lstUsers = new List<Models.User>();
                lstUsers = oUser.GetUserInformation(Convert.ToInt64(Session["UserID"]),"ONE");
                return View(lstUsers[0]);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Models.User oUser)
        {
            if (ModelState.IsValid)
            {
                if (!oUser.Password.Equals(oUser.ConfirmPassword, StringComparison.CurrentCultureIgnoreCase))
                {
                    Int32 nOutput = oUser.ChangePassword(oUser.User_ID, oUser.Password, oUser.ConfirmPassword);
                    switch (nOutput)
                    {
                        case 1: ViewBag.Message = "Password changed successfully";
                            break;
                        case -1: ViewBag.Message = "User is not valid.";
                            break;
                        case -2: ViewBag.Message = "Error occurred while changing password.";
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "New password not be same as current password");
                }
                //if (oUser.CheckValidPassword(oUser.User_ID,oUser.Password))
                //{
                    
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Current password does not match with our record. Please enter correct password");
                //}
            }
            return View();
        }
        [HttpGet]
        public ActionResult ViewProfile(Int64 UserID)
        {
            Models.User oUser = new Models.User();
            List<Models.User> lstUsers = null;
            if (ModelState.IsValid)
            {
                lstUsers = oUser.GetUserInformation(UserID, "ONE");
                if (lstUsers!=null&&lstUsers.Count>0)
                {
                    oUser = lstUsers[0]; 
                }
                ViewBag.RoleList = new SelectList(GetAllRoles(), "RoleID", "RoleName");
            }
            ViewBag.UserID = UserID;
            return View(oUser);
        }

        [HttpPost]
        public ActionResult ViewProfile(Models.User oUser)
        {
            string sTask = string.Empty;
            if (ModelState.IsValid)
            {
                int nStatus;
                if (oUser.MiddleName == null)
                {
                    oUser.MiddleName = "";
                }
                if (oUser.User_ID==0)
                {
                    nStatus = oUser.InsertUpdateDeleteUser(oUser, "NEW");
                }
                else
                {
                    nStatus = oUser.InsertUpdateDeleteUser(oUser, "EDIT");
                }
                switch (nStatus)
                {
                    case -1: ViewBag.Message = "User already exists";
                        break;
                    case 0: ViewBag.Message = "User registerd successfully";
                        break;
                    case 1: ViewBag.Message = "Error";
                        break;
                    case 2: ViewBag.Message = "User profile updated successfully";
                        break;
                    case 3: ViewBag.Message = "User deleted successfully";
                        break;
                }
                //if (oUser.InsertUpdateDeleteUser(oUser, "sTask") == 2)
                //{
                //    ViewBag.Message = "User profile updated successfully";
                //}
                //else
                //{
                //    ModelState.AddModelError("","Error in user profile update");
                //}
                ViewBag.RoleList = new SelectList(GetAllRoles(), "RoleID", "RoleName");
            }
            return View(oUser);
        }
        public ActionResult Delete(Int64 UserID)
        {
            Models.User oUser = new Models.User();
            if (ModelState.IsValid)
            {
                int nStatus = 0;
                if (UserID != 0)
                {
                    nStatus = oUser.DeleteUser(UserID, "DELETE");
                }
                switch (nStatus)
                {
                    case -1: ViewBag.Message = "User information already exists";
                        break;
                    case 1: ViewBag.Message = "Error";
                        break;
                    case 3: ViewBag.Message = "User information deleted successfully";
                        break;
                }
            }
            ViewBag.Action = "ManageUser";
            ViewBag.ControllerName = "Account";
            return View();
        }
        private List<Models.Role> GetAllRoles()
        {
            List<Models.Role> lstRoles = null;
            if (ModelState.IsValid)
            {
                Models.Role oRole = new Models.Role();
                lstRoles = oRole.GetAllRoles(0,"ALL");
            }
            return lstRoles;
        }
        [HttpGet]
        public ActionResult CompanyProfile()
        {
            Models.Company oCompany = new Models.Company();
            List<Models.Company> lstCompany = null;
            if (ModelState.IsValid)
            {
                lstCompany = oCompany.GetCompanyInformation();
                if (lstCompany != null && lstCompany.Count > 0)
                {
                    oCompany = lstCompany[0];
                }
            }
            return View(oCompany);
        }
        [HttpPost]
        public ActionResult CompanyProfile(Models.Company oCompany)
        {
            if (ModelState.IsValid)
            {
                if (oCompany.InsertUpdateCompnay(oCompany, "UPDATE") == 2)
                {
                    ViewBag.Message = "Company profile updated successfully";
                }
                else
                {
                    ModelState.AddModelError("", "Error in company profile update");
                }
            }
            return View(oCompany);
        }
        [HttpGet]
        public ActionResult ManageUser()
        {
            Models.User oUser = new Models.User();
            List<Models.User> lstUsers = null;
            if (ModelState.IsValid)
            {
                lstUsers = oUser.GetUserInformation(0,"ALL");
            }
            ViewBag.UserList = lstUsers;
            return View();
        }
        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }
    }
}