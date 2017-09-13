namespace myfhaemailadmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.DynamicData;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(clientMD))]
    public partial class client : DbContext
    {
    }

    public class clientMD
    {

        [Display(Name = "Client Name")]
        public string name { get; set; }
    }
}