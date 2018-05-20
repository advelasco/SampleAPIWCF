namespace Sample.Repository
{
    using Sample.Model;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    //using System.Data.Entity.Infrastructure.Annotations;

    public class SampleContext : DbContext
    {
        public SampleContext() : base("SampleConnectionString")
        {
            //Database.SetInitializer<SampleContext>(new CreateDatabaseIfNotExists<SampleContext>());
            //Database.SetInitializer<SampleContext>(new DropCreateDatabaseAlways<SampleContext>());

            Database.SetInitializer<SampleContext>(new DropCreateDatabaseIfModelChanges<SampleContext>());
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserModel>()
            //     .HasMany<SubscriptionModel>(g => g.Subscription)
            //.WithRequired(g => g.User)
            //.HasForeignKey<Guid>(s => s.UserRefId);

            //modelBuilder.Entity<UserModel>().Property(x => x.UserId).IsRequired()
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
        }
    }
}
