using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Controllers._001_Controllers
{
    public class _004_ReceiveDataSimpleTypesController : Controller
    {
        // Платформа предоставит значения для параметров, проверив соответствующие объекты контекста,
        // включая Request.Form, RouteData.Values, Request.QueryString

        // GET: /_004_ReceiveDataSimpleTypes/GetDataFromRequest
        public ActionResult GetDataFromRequestSimpleType()
        {
            return View("001_Controller/_004_ReceiveDataSimpleTypes/GetDataFromRequestSimpleType");
        }

        // POST: /_004_ReceiveDataSimpleTypes/PostInformationSimpleType
        public ActionResult PostInformationSimpleType(string message)
        {
            //ViewBag.Message = Request.Form["message"];
            ViewBag.Title = "PostInformationSimpleType";
            ViewBag.Message = message;
            return View("Index");
        }

        // GET: /_004_ReceiveDataSimpleTypes/RouteInformationSimpleType
        public ActionResult RouteInformationSimpleType(string id)
        {
            //ViewBag.Message = RouteData.Values["id"];
            ViewBag.Title = "RouteInformationSimpleType";
            ViewBag.Message = id;
            return View("Index");
        }

        // GET: /_004_ReceiveDataSimpleTypes/QueryInformationSimpleType
        public ActionResult QueryInformationSimpleType(string message)
        {
            //ViewBag.Message = Request.QueryString["message"];
            ViewBag.Title = "QueryInformationSimpleType";
            ViewBag.Message = message;
            return View("Index");
        }

        // GET: /_004_ReceiveDataSimpleTypes/SimpleType/5
        // GET: /_004_ReceiveDataSimpleTypes/SimpleType?id=5
        public ActionResult SimpleType(int id)
        {
            ViewBag.Message = "id = " + id;
            return View("Index");
        }
        //can use only variable name like in routes with Html.ActionLink
        // GET: /_004_ReceiveDataSimpleTypes/SimpleType?ff=5
        public ActionResult SimpleType2(int ff)
        {
            ViewBag.Message = "ff = " + ff;
            return View("Index");
        }

        // GET: /_004_ReceiveDataSimpleTypes/SimpleForm
        public ActionResult SimpleForm()
        {
            string x = Request.Params["x"];
            string y = Request.Params["y"];
            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                ViewBag.Result = x + y;
            }
            return View("001_Controller/_004_ReceiveDataSimpleTypes/SimpleForm");
        }

        // POST: /_004_ReceiveDataSimpleTypes/SimpleForm
        [HttpPost] // метод будет вызываться только при POST запросах
        public ActionResult SimpleForm(int x, int y)
        {
            ViewBag.Result = x + y;

            return View("001_Controller/_004_ReceiveDataSimpleTypes/SimpleForm");
        }

    }
}
