using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BMEApplication.ActionFilters
{
    public class ViewActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session["UserID"]!=null)
            {
                SetViewByUserRole(filterContext, session);
            }
            else
            {
                if (!filterContext.RouteData.Values.ContainsValue("Login"))
                {
                    if (!filterContext.RouteData.Values.ContainsValue("ForgotPassword"))
                    {
                        filterContext.Result = new RedirectResult("/Account");
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private void SetViewByUserRole(ActionExecutingContext filterContext, HttpSessionStateBase session)
        {
            if (session["UserID"]!=null&&session["UserName"]!=null&&session["RoleID"]!=null&&session["RoleName"]!=null)
            {
                bool bIsValidUser = false;
                bool bIsValidRole = false;
                Models.User oUser = null;
                Models.Role oRole=null;
                try
                {
                    oUser = new Models.User();
                    List<Models.User> oUserList= oUser.GetUserInformation(Convert.ToInt64(session["UserID"]),"ONE");
                    if (oUserList.Count==1)
                    {
                        bIsValidUser = true;
                    }
                    oRole = new Models.Role();
                    List<Models.Role> oRoleList= oRole.GetAllRoles(Convert.ToInt64(session["RoleID"]),"ONE");
                    if (oRoleList.Count==1)
                    {
                        bIsValidRole = true;
                    }

                    if (bIsValidUser)
                    {
                        if (bIsValidRole)
                        {
                            switch (Convert.ToString(session["RoleName"]))
                            {
                                case "Super Admin":
                                    filterContext.Controller.ViewData["Dashboard"] = "normal";
                                    filterContext.Controller.ViewData["ManageAircraft"] = "normal";
                                    filterContext.Controller.ViewData["ManageAirline"] = "normal";
                                    filterContext.Controller.ViewData["StandData"] = "normal";
                                    filterContext.Controller.ViewData["AssociationData"] = "normal";
                                    break;
                                case "Admin":
                                    filterContext.Controller.ViewData["Dashboard"] = "normal";
                                    filterContext.Controller.ViewData["ManageAircraft"] = "normal";
                                    filterContext.Controller.ViewData["ManageAirline"] = "normal";
                                    filterContext.Controller.ViewData["StandData"] = "normal";
                                    filterContext.Controller.ViewData["AssociationData"] = "normal";
                                    break;
                                case "User":
                                    filterContext.Controller.ViewData["Dashboard"] = "normal";
                                    filterContext.Controller.ViewData["ManageAircraft"] = "normal";
                                    filterContext.Controller.ViewData["ManageAirline"] = "normal";
                                    filterContext.Controller.ViewData["StandData"] = "normal";
                                    filterContext.Controller.ViewData["AssociationData"] = "normal";
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    if (oUser!=null)
                    {
                        oUser.Dispose();
                        oUser = null;
                    }
                    if (oRole!=null)
                    {
                        oRole.Dispose();
                        oRole = null;
                    }
                }
            }
        }
    }
}