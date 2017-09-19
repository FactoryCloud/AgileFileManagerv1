using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<Employee> employees { get; set; }
        public virtual List<Issue> issues { get; set; }
    }
}
