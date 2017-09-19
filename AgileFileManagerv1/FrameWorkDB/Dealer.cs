using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Dealer
    {
        [Key]
        [Required]
        public int DealerID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public virtual List<Client> clients { get; set; }
    }
}
