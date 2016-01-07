using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JCStratJobApp.Models;
using JCStratJobApp.Data;
using Kendo.Mvc.Extensions;

namespace JCStratJobApp.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        private Data.JCStratDBEntities _context = new Data.JCStratDBEntities();
        public ActionResult Index()
        //new Models.people
        {
            var people = _context.TBLPeoples.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone_Number = p.Phone_Number


            });
            return this.Json(people, JsonRequestBehavior.AllowGet);
           
        }


        public ActionResult people_Read([DataSourceRequest]DataSourceRequest request)
        {
            var results = _context.TBLPeoples.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone_Number = p.Phone_Number


            });
            IQueryable<people> peoples = _context.TBLPeoples.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone_Number = p.Phone_Number


            }).Cast<people>();
            DataSourceResult result = peoples.ToDataSourceResult(request, person => new {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Phone_Number = person.Phone_Number
            });

            return this.Json(result, JsonRequestBehavior.AllowGet);

          
            // return Json(result);
         }


    }
}