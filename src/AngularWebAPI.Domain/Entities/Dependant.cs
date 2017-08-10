using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Domain.Entities
{
    public class Dependant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        [Required(ErrorMessage ="Please Select an Employee")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Firstname is Required")]
        [StringLength(50, ErrorMessage = "Maximum required string length is 50")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Lastname is Required")]
        [StringLength(50, ErrorMessage = "Maximum required string length is 50")]
        public string Lastname { get; set; }

        [Required(ErrorMessage ="Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Relationship is Required")]
        [StringLength(50, ErrorMessage = "Maximum required string length is 50")]
        public string Relationship { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
