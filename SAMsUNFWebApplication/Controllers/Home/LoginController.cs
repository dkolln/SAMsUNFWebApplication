﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAMsUNFWebApplication.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using SAMsUNFWebApplication.Models.DataAccess;

namespace SAMsUNFWebApplication.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Login(Profile model, string actionRequest)
        {

            if (model.UserID != null && model.UserID.Length > 0 && model.Password != null && model.Password.Length > 0)
            {
                //  model = new KIPPDemoDAO().LoginValidation(model.UserID, model.Password);


                using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString))
                {
                    await connection.OpenAsync();

                    model = await new UserRepository(connection).LoginValidation(model.UserID, model.Password);

             
                }
                if (model == null)
                {
                    ModelState.AddModelError(string.Empty, "UserId or Password is invalid,please retry.");
                }
                else
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }
            }
                return View(model);
        }
    }
}