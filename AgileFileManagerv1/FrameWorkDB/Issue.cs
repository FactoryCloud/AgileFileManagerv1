using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Issue
    {
        [Key]
        [Required]
        public int IssueID { get; set; }

        public int? Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("FK_Issues_DepartmentID_Departments")]
        public int? DepartmentID { get; set; }
        public virtual Department department { get; set; }

        public virtual List<File> files { get; set; }
    }
}
