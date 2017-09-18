using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Dealer
    {
        [Key]
        [Required]
        public int DealerID { get; set; }

        public virtual List<Client> clients { get; set; }
    }
}
