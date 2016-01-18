using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace JCStratJobApp.Models
{
    //Class for the getter and setter, and validation, for the attributes of people in the database.
    public class people
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required(ErrorMessage = "Telephone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        //This validation is for typical phones in Canada and States, not sure there's something for all places.
        public string Phone_Number { get; set; }
    }

}