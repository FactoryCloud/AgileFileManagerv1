﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Priority
    {
        [Key]
        [Required]
        public int PriorityID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<File> files { get; set; }
    }
}
