using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;


namespace WebPhoneMobile.Models
{
    public class ViewModel
    {
        public client client { get; set; }
        public billdetail billdetail { get; set; }
        public bill bill { get; set; }
        public category category { get; set; }
        public product product { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##0}")]
        public double amount { get; set; }
    }
}