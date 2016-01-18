using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JCStratJobApp.Data;
using System.Data;


namespace JCStratJobApp.Models
{
    public class peopleRepository
    {
        public static IList<people> All()
        {
            var result = HttpContext.Current.Session["TBLPeople"] as IList<people>;

            if (result == null)
            {
                HttpContext.Current.Session["TBLPeople"] = result =
                    new JCStratDBEntities().TBLPeoples.Select(p => new people
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Address = p.Address,
                        Phone_Number = p.Phone_Number

                    }).ToList();
            }

            return result;
        }



        public static people One(Func<people, bool> predicate)
        {
            return All().FirstOrDefault(predicate);
        }

        public static void Insert(people person)
        {
            var first = All().OrderByDescending(p => p.Id).FirstOrDefault();
            if (first != null)
            {
                person.Id = first.Id + 1;
            }
            else
            {
                person.Id = 0;
            }

            All().Insert(0, person);
        }

        public static void Insert(IEnumerable<people> persons)
        {
            foreach (var person in persons)
            {
                Insert(person);
            }
        }

        public static void Update(people person)
        {
            var target = One(p => p.Id == person.Id);
            if (target != null)
            {
                target.Name = person.Name;
                target.Address = person.Address;
                target.Phone_Number = person.Phone_Number;
        
            }
        }

        public static void Update(IEnumerable<people> persons)
        {
            foreach (var person in persons)
            {
                Update(person);
            }
        }

        public static void Delete(people person)
        {
            var target = One(p => p.Id == person.Id);
            if (target != null)
            {
                All().Remove(target);
            }
        }

        public static void Delete(IEnumerable<people> persons)
        {
            foreach (var person in persons)
            {
                Delete(person);
            }
        }
    }
}