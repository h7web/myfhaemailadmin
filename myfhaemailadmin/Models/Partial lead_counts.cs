using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;

namespace myfhaemailadmin.Models
{
    [MetadataType(typeof(lead_countsMD))]
    public partial class lead_counts: DbContext
    {
    }
    public class lead_countsMD
    {
        [Display(Name = "ID")]
        public int id{ get; set; }
        public string Client { get; set; }
        [Display(Name = "Excellent")]
        public string ex{ get; set; }

        [Display(Name = "Good")]
        public string gd{ get; set; }

        [Display(Name = "Some Problems")]
        public string sp{ get; set; }

        [Display(Name = "Many Problems")]
        public string mp{ get; set; }

        [Display(Name = "Special Circumstances")]
        public string sc { get; set; }

        [Display(Name = "Total")]
        public string total { get; set; }

    }
}