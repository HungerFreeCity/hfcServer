namespace hfcServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodBank()
        {
            Needs = new HashSet<Need>();
        }

        public int Id { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string WebsiteURL { get; set; }

        public double Latitude { get; set; }

        public string ManagerEmail { get; set; }

        public double Longtitude { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Need> Needs { get; set; }
    }
}
