using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Application
    {
        [Key]
        [Required]
        public int ApplicationID { get; set; }

        public virtual List<License> licenses { get; set; }
    }
}
