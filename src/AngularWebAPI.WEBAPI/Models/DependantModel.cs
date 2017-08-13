using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularWebAPI.WEBAPI.Models
{
    public class DependantModel
    {
        public int ID { get; set; }

       
        public int EmployeeID { get; set; }

       
        public string Firstname { get; set; }       
        public string Lastname { get; set; }
      
        public string Gender { get; set; }
       
        public string Relationship { get; set; }

    }
}