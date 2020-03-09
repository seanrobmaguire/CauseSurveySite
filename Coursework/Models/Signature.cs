using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Signature
    {
        public int SignatureID { get; set; }
        public int CauseID { get; set; }
        public int UserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime signedOn { get; set; }


        public virtual Cause Cause { get; set; }
        public virtual User User { get; set; }

    }
}