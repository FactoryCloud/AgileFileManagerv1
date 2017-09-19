using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Intervention
    {
        [Key]
        [Required]
        public int InterventionID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_Interventions_EmployeeID_Employees")]
        public int? EmployeeID { get; set; }
        public virtual Employee employee { get; set; }

        [ForeignKey("FK_Interventions_FileID_Files")]
        public int? FileID { get; set; }
        public virtual File file { get; set; }
    }
}
