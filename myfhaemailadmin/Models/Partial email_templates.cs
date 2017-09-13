namespace myfhaemailadmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web.DynamicData;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(email_templatesMD))]
    public partial class email_templates : DbContext
    {
        public System.Data.Entity.DbSet<myfhaemailadmin.Models.email_templatesMD> email_templatesMD { get; set; }
    }

    public class email_templatesMD
    {
        [Key]
        public int emailid { get; set; }
        [Required]
        [Range(1,999)]
        [Display(Name = "Days Since Submission")]
        public Nullable<int> days { get; set; }
        [Display(Name = "Client")]
        [Required]
        public Nullable<int> client_id { get; set; }
        [Required]
        [Display(Name = "Credit Rating")]
        public string credit { get; set; }
        [Required]
        [Display(Name="Subject")]
        public string subject { get; set; }
        [Required]
        [AllowHtml]
        public string html { get; set; }

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
        public Boolean enable_modified { get; set; }
        public string toemail { get; set; }
    }
}