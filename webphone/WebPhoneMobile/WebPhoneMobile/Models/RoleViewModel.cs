using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebPhoneMobile.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}