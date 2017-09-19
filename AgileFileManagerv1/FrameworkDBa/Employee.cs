using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Employee
    {
        [Key]
        [Required]
        public int EmployeeID { get; set; }

        [ForeignKey("FK_Employees_DepartmentID_Departments")]
        public int? DepartmentID { get; set; }
        public virtual Department department { get; set; }

        public virtual List<File> files { get; set; }
        public virtual List<Intervention> interventions { get; set; }
        public virtual List<Report> reports { get; set; }
    }
}
