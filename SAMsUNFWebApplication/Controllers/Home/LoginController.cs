﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAMsUNFWebApplication.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using SAMsUNFWebApplication.Models.DataAccess;
using System.Web.Security;

namespace SAMsUNFWebApplication.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Login(ProfileModel model, string actionRequest)
        {

            if (model.user_name != null && model.user_name.Length > 0 && model.password != null && model.password.Length > 0)
            {
                using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
                {
                    await connection.OpenAsync();

                    model = await new UserRepository(connection).LoginValidation(model.user_name, model.password);

             
                }
                if (model == null)
                {
                    FormsAuthentication.SignOut();
                    ModelState.AddModelError(string.Empty, "UserId or Password is invalid,please retry.");
                }
                else
                {
                  
                    FormsAuthentication.SetAuthCookie(model.user_name,false);
                    //insert session veriable here
                    //example: List<Student> students = (List<Student>)Session["Students"];
                    return RedirectToAction ("Dashboard", "Dashboard");
                }
            }
                return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Index");

        }
    }
}