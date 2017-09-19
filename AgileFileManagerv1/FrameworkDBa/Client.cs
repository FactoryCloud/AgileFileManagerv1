using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB
{
    public class Client
    {
        [Key]
        [Required]
        public int ClientID { get; set; }

        [ForeignKey("FK_Clients_DealerID_Dealers")]
        public int? DealerID { get; set; }
        public virtual Dealer dealer { get; set; }

        public virtual List<File> files { get; set; }
        public virtual List<License> licenses { get; set; }
    }
}
