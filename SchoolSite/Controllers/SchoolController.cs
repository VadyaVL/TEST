﻿using SchoolSite.Domain.Core;
using SchoolSite.Domain.DTO;
using SchoolSite.Infrastructure.Business;
using SchoolSite.Service;
using SchoolSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSite.Controllers
{
    public class SchoolController : Controller
    {
        private ISchoolService schoolService;

        public SchoolController(ISchoolService ss) : base()
        {
            schoolService = ss;
        }

        public ActionResult Index()
        {
            return View("school");
        }

        public ActionResult JSON_ALL_School()
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            return Json(schoolService.GetSchoolFeed(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult JSON_School(bool get = false, int count = 0)
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            if(get)
                return Json(schoolService.GetSchoolFeed(count + 10), JsonRequestBehavior.AllowGet);
            else
                return Json(schoolService.GetSchoolFeed(count), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostSchool(int id, String name)
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (id == 0)
            {
                SchoolViewModel s = new SchoolViewModel();
                s.Name = name;

                schoolService.Save(s);
            }
            else
            {
                SchoolViewModel fSchool = null;

                foreach (SchoolViewModel sc in schoolService.GetAll())
                {
                    if (sc.Id == id)
                    {
                        fSchool = sc;
                        break;
                    }
                }

                if (fSchool != null)
                {
                    fSchool.Name = name;
                    schoolService.Update(fSchool);
                }
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveSchool(int id)
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            SchoolViewModel fSchool = null;

            foreach(SchoolViewModel sc in schoolService.GetAll())
            {
                if(sc.Id == id)
                {
                    fSchool = sc;
                    break;
                }
            }

            if (fSchool != null)
            {
                schoolService.Delete(fSchool.Id);
            }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}