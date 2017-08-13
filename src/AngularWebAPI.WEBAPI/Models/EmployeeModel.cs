using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularWebAPI.WEBAPI.Models
{
    public class EmployeeModel
    {
       
        public int EmployeeID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }


        public string Position { get; set; }
              
        public string Gender { get; set; }

        public List<DependantModel> Dependants { get; set; }
    }
}