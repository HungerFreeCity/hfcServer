namespace hfcServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public string DonorId { get; set; }

        public int NeedId { get; set; }

        public virtual Need Need { get; set; }
    }
}
