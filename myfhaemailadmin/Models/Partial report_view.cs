namespace myfhaemailadmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web.DynamicData;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(report_viewMD))]
    public partial class report_view : DbContext
    {
    }

    public class report_viewMD
    {
        [Display(Name = "ID")]
        public int emailid { get; set; }
        public string Client { get; set; }
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Display(Name = "24 Hours")]
        public int cnt_24_Hours { get; set; }
        [Display(Name = "7 Days")]
        public int cnt_7_Days { get; set; }
        [Display(Name = "1 Month")]
        public int cnt_1_Month { get; set; }
        [Display(Name = "6 Months")]
        public int cnt_6_Months { get; set; }
        public int Total { get; set; }
        public Nullable<bool> enabled { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> enable_modified { get; set; }
    }
}