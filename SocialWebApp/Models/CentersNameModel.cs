using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialWebApp.Models
{
    public class CentersNameModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Choose Your Education Center Name")]
        public string CenterName { get; set; }
    }
}