﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Issue
    {
        [Key]
        [Required]
        public int IssueID { get; set; }

        [ForeignKey("FK_Issues_DepartmentID_Departments")]
        public int? DepartmentID { get; set; }
        public virtual Department department { get; set; }

        public virtual List<File> files { get; set; }
    }
}
