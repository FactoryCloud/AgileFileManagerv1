using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorkDB.V1
{
    public class License
    {
        [Key]
        [Required]
        public int LicenseID { get; set; }

        public int? Code { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        [ForeignKey("FK_Licenses_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }

        [ForeignKey("FK_Licenses_ApplicationID_Applications")]
        public int? ApplicationID { get; set; }
        public virtual Application application { get; set; }

        public virtual List<File> files { get; set; }
    }
}
