using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class Client
    {
        [Key]
        [Required]
        public int ClientID { get; set; }

        public int? Code { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string SubName { get; set; }

        [StringLength(200)]
        public string Contact { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone1 { get; set; }

        [StringLength(20)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Mail1 { get; set; }

        [ForeignKey("FK_Clients_DealerID_Dealers")]
        public int? DealerID { get; set; }
        public virtual Dealer dealer { get; set; }

        public virtual List<File> files { get; set; }
        public virtual List<License> licenses { get; set; }
    }
}
