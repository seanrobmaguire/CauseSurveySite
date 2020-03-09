using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string Name { get; set;}

        public string catImage { get; set; }


        public ICollection<Cause> Causes { get; set; }
     }
}