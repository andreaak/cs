﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _011_HttpCodesResultController : Controller
    {
        //
        // GET: /_001_Controller/_011_HttpCodes/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActionHttpNotFound()
        {
            // Возвращение статуса 404 NotFound
            return HttpNotFound();
        }

        public ActionResult ActionServerError()
        {
            int result;
            try
            {
                int a = 10, b = 0;
                result = a / b;
            }
            catch (Exception)
            {
                // Возвращение статуса 500 Internal Server Error
                return new HttpStatusCodeResult(500, "Internal Server Error");
            }

            return Content(result.ToString());
        }
    }
}