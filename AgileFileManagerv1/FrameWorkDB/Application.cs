﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Application
    {
        [Key]
        [Required]
        public int ApplicationID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public decimal? Version { get; set; }

        [ForeignKey("FK_Applications_DepartmentID_Departments")]
        public int? DepartmentID { get; set; }
        public virtual Department department { get; set; }

        public virtual List<License> licenses { get; set; }
    }
}
