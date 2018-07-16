using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AssetVerificationApi.Models;
///using AssetVerificationApi.Models.AssetCategory;

namespace AssetVerificationApi.Context_
{
    public class Context : DbContext
    {
        public Context() : base("name=AssetVerificationApiConnectionString")
        {
            Database.SetInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
        }

        public DbSet<AssetModel> AssetModel { get; set; }
        public DbSet<SiteModel> SiteModel { get; set; }
        public DbSet<FieldDataModel> FieldDataModel { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<ParentAssetModel> ParentAsset { get; set; }
        public DbSet<PropertyModel> Property { get; set; }
        public DbSet<AssetChild> AssetChild { get; set; }
        public DbSet<PropertyValue> PropertyValue { get; set; }
        public DbSet<OptionModel> Option { get; set; }
        public DbSet<Fake> Fake { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteModel>().Property(a => a.Latitude).HasPrecision(18, 9);
            modelBuilder.Entity<SiteModel>().Property(a => a.Longitude).HasPrecision(18, 9);
        }
    }
}
