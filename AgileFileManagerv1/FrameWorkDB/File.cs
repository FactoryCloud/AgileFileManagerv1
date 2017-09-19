using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class File
    {
        [Key]
        [Required]
        public int FileID { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        [ForeignKey("FK_Files_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }

        [ForeignKey("FK_Files_EmployeeID_Employees")]
        public int? EmployeeID { get; set; }
        public virtual Employee employee { get; set; }

        [ForeignKey("FK_Files_LicenseID_Licenses")]
        public int? LicenseID { get; set; }
        public virtual License license { get; set; }

        [ForeignKey("FK_Files_IssueID_Issues")]
        public int? IssueID { get; set; }
        public virtual Issue issue { get; set; }

        [ForeignKey("FK_Files_StateID_States")]
        public int? StateID { get; set; }
        public virtual State state { get; set; }

        [ForeignKey("FK_Files_PriorityID_Priorities")]
        public int? PriorityID { get; set; }
        public virtual Priority priority { get; set; }

        public virtual List<Intervention> interventions { get; set; }
        public virtual List<Report> reports { get; set; }
    }
}
