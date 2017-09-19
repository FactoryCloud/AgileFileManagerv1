using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Report
    {
        [Key]
        [Required]
        public int ReportID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_Reports_EmployeeID_Employees")]
        public int? EmployeeID { get; set; }
        public virtual Employee employee { get; set; }

        [ForeignKey("FK_Reports_FileID_Files")]
        public int? FileID { get; set; }
        public virtual File file { get; set; }
    }
}