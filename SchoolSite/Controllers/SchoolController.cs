﻿using SchoolSite.Models;
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
        public ActionResult Index()
        {
            return View("school");
        }


        public ActionResult JSON_School()
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            return Json(SchoolDBContext.GetInstance().Schools, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostSchool(int id, String name)
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (id == 0)
            {
                School s = new School();
                s.Name = name;

                SchoolDBContext.GetInstance().Schools.Add(s);
            }
            else
            {
                School fSchool = null;

                foreach (School sc in SchoolDBContext.GetInstance().Schools)
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
                }
            }
            SchoolDBContext.GetInstance().SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveSchool(int id)
        {
            this.ControllerContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            School fSchool = null;

            foreach(School sc in SchoolDBContext.GetInstance().Schools)
            {
                if(sc.Id == id)
                {
                    fSchool = sc;
                    break;
                }
            }

            if (fSchool != null)
            {
                SchoolDBContext.GetInstance().Schools.Remove(fSchool);
                SchoolDBContext.GetInstance().SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}