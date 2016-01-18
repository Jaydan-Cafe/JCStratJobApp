using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JCStratJobApp.Models;
using JCStratJobApp.Data;
using System.Web.Script.Serialization;
using JCStratJobApp.Common;
namespace JCStratJobApp.Controllers
{
    public class JCPeopleController : Controller
    {

        private JCStratDBEntities db = new JCStratDBEntities();


        public ActionResult Index()
        {

            
            return this.Jsonp(peopleRepository.All());

        }

        //Reads the data from the database and returns the information.

        public JsonResult people_Read(int skip, int take)
        {

            IEnumerable<people> result = peopleRepository.All().OrderByDescending(p => p.Id);

            result = result.Skip(skip).Take(take);

            return this.Jsonp(result);



        }



        public ActionResult people_Create()
        {
            var persons = this.DeserializeObject<IEnumerable<people>>("models");
            if (persons != null)
            {
                peopleRepository.Insert(persons);
            }
            return this.Jsonp(persons);
        }




        public JsonResult people_Update()
        {
            var models = this.DeserializeObject<IEnumerable<people>>("models");
            if (models != null)
            {
                peopleRepository.Update(models);
            }
            return this.Jsonp(models);

        }


        public ActionResult people_Destroy()
        {
            var persons = this.DeserializeObject<IEnumerable<people>>("models");

            if (persons != null)
            {
                peopleRepository.Delete(persons);
            }
            return this.Jsonp(persons);

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

