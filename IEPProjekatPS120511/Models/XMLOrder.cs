using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IEPProjekatPS120511.Models
{
    public class XMLOrder
    {
        [Required]
        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }


    }
}