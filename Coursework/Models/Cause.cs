using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Cause
    {
        public int CauseID { get; set; }

        [DisplayName("Title:")]
        [Required(ErrorMessage = "A Title is required.")]
        [StringLength(160, ErrorMessage = "Title must be less than 160 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A Description is required.")]
        [StringLength(10000, MinimumLength = 100, ErrorMessage = "Description must be longer than be between 100 and 10000 characters.")]
        [DisplayName("Description:")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int UserID { get; set; }

        [DisplayName("Created by:")]
        public string createdBy { get; set; }

        [DisplayName("Created on:")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime createdOn { get; set; }

        [Required(ErrorMessage = "A Target number of Signatures is required.")]
        [DisplayName("Signature Target:")]
        public int Target { get; set; }

        public int CategoryID { get; set; }

        [DisplayName("No. of Signatures:")]
        [DisplayFormat(NullDisplayText = "No signatures yet, be the first!")]
        public virtual ICollection<Signature> Signatures { get; set; }

        public Category Category { get; set; }
    }
}