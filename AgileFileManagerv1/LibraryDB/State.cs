using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class State
    {
        [Key]
        [Required]
        public int StateID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<File> files { get; set; }
    }
}
