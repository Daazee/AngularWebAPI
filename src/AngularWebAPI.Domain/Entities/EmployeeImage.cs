using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Domain.Entities
{
    public class EmployeeImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public byte[] Image { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
