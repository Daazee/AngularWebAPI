using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Domain.Entities
{
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage ="Firstname is Required")]
        [StringLength(50, ErrorMessage ="Maximum required string length is 50")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is Required")]
        [StringLength(50, ErrorMessage = "Maximum required string length is 50")]
        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Firstname is Required")]               
        public string Position { get; set; }

        [Required(ErrorMessage = "Firstname is Required")]        
        public string Gender { get; set; }
    }
}
