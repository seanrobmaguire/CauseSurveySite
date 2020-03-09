using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public enum Role
    {
        admin, user
    }
    public class User
    {
        public int ID { get; set; }

        [Required (ErrorMessage="A Username is required.")]
        [DisplayName("Username:")]
        [StringLength(10000, MinimumLength = 8, ErrorMessage = "Username must be longer than 8 characters and include a number and capital")]

        public string Username { get; set; }

        [Required (ErrorMessage ="An Email Address is required.")]
        [EmailAddress]
        [DisplayName("Email:")]

        public string Email { get; set; }

        [Required (ErrorMessage ="A Password is required.")]
        [StringLength(10000, MinimumLength = 8, ErrorMessage = "Password must be longer than 8 characters and include a number and capital")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("User Image:")]
        public string UserImage { get; set; }

        public Role Role { get; set; }

        [DisplayName("Causes Signed:")]
        [DisplayFormat(NullDisplayText = "No Causes signed yet.")]
        public virtual ICollection<Signature> Signatures { get; set; }
    }
}