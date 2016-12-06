namespace hfcServer.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NeedModel : DbContext
    {
        public NeedModel()
            : base("name=NeedModel")
        {
        }

        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<FoodBank> FoodBanks { get; set; }
        public virtual DbSet<Need> Needs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
