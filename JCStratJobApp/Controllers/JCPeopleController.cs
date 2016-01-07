using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JCStratJobApp.Models;
using JCStratJobApp.Data;


namespace JCStratJobApp.Controllers
{
    public class JCPeopleController : Controller
    {
        private JCStratDBEntities db = new JCStratDBEntities();

        public ActionResult Index()
        {
            var result = db.TBLPeoples.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Phone_Number = p.Phone_Number


            });
            return View(result);
          
        }

        //Reads the data from the database and returns the information.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult people_Read([DataSourceRequest]DataSourceRequest request)
        {



            using (var Strat = new JCStratDBEntities())
            {
                IQueryable<TBLPeople> people = Strat.TBLPeoples;
                DataSourceResult result = people.ToDataSourceResult(request);
                return Json(result);
            }


          
        }


        //Handles adding new people to the database
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult people_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<people> people)
        {
            var entities = new List<TBLPeople>();
           
            if (people != null && ModelState.IsValid)
            {
                using (var Strat = new JCStratDBEntities())
                {
                    foreach (var person in people)
                    {
                        
                        var entity = new TBLPeople
                        {
                            Id = person.Id,
                            Name = person.Name,
                            Address = person.Address,
                            Phone_Number = person.Phone_Number
                        };
                        // Add the entity
                        Strat.TBLPeoples.Add(entity);
                        // Store the entity for later use
                        entities.Add(entity);
                    }
                    // Insert the entities in the database
                    Strat.SaveChanges();
                }
            }

            return Json(entities.ToDataSourceResult(request, ModelState, person => new people
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Phone_Number = person.Phone_Number
            }));
        }

        //Handles updates
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult people_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<people> people)
        {

            var entities = new List<TBLPeople>();
          
            if (people != null && ModelState.IsValid)
            {
                using (var Strat = new JCStratDBEntities())
                {
                    foreach (var person in people)
                    {

                        var entity = new TBLPeople
                        {
                            Id = person.Id,
                            Name = person.Name,
                            Address = person.Address,
                            Phone_Number = person.Phone_Number
                        };
                        
                       
                        Strat.TBLPeoples.Attach(entity);
                        Strat.Entry(entity).State = EntityState.Modified;

                    }
                    // Insert the entities in the database
                    Strat.SaveChanges();
                }
            }

            return Json(entities.ToDataSourceResult(request, ModelState, person => new people
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Phone_Number = person.Phone_Number
            }));

        }
        //Handles deletetion
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult people_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<people> people)
        {
            var entities = new List<TBLPeople>();
            
            if (people != null && ModelState.IsValid)
            {
                using (var Strat = new JCStratDBEntities())
                {
                    foreach (var person in people)
                    {

                        var entity = new TBLPeople
                        {
                            Id = person.Id,
                            Name = person.Name,
                            Address = person.Address,
                            Phone_Number = person.Phone_Number
                        };

                        //entities.Add(entity);
                        Strat.TBLPeoples.Attach(entity);
                        Strat.TBLPeoples.Remove(entity);

                    }
                    // Delete the entities in the database
                    Strat.SaveChanges();
                }
            }

            return Json(entities.ToDataSourceResult(request, ModelState, person => new people
            {
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Phone_Number = person.Phone_Number
            }));

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
