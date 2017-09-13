namespace myfhaemailadmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web.DynamicData;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(template_clientMD))]
    public partial class template_client : DbContext
    {
    }

    public class template_clientMD
    {
        [Display(Name = "Days Since Submission")]
        public Nullable<int> days { get; set; }
        [Display(Name = "Client")]
        public Nullable<int> client_id { get; set; }
        [Display(Name = "Credit Rating")]
        public string credit { get; set; }
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [AllowHtml]
        public string html { get; set; }
        [Display(Name = "Client")]
        public string name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Created On")]
        public Nullable<System.DateTime> created_on { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> last_modified { get; set; }

        [Display(Name = "Enabled")]
        public Boolean enabled { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Enable Status Modified")]
        public DateTime enable_modified { get; set; }

        [Display(Name = "Number of Emails Sent")]
        public Nullable<int> cnt { get; set; }
        public string toemail { get; set; }
    }
}