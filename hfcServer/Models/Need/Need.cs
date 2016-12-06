namespace hfcServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Need
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Need()
        {
            Donations = new HashSet<Donation>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public string Units { get; set; }

        public bool Display { get; set; }

        public int FoodBankId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation> Donations { get; set; }

        public virtual FoodBank FoodBank { get; set; }
    }
}
