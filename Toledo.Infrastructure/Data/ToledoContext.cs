using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Toledo.Infrastructure.Data
{
    public class ToledoContext : DbContext
    {
        public  DbSet<User> Users { get; set; }
        public  DbSet<Location> Locations { get; set; }
        public  DbSet<Pet> Pets { get; set; }
        public  DbSet<PetImage> PetImages { get; set; }
        public  DbSet<PetDisease> PetDiseases { get; set; }

        public ToledoContext(){}

        public ToledoContext(DbContextOptions<ToledoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
             
            var connectionString = $"Server={dbHost};port={dbPort};user id={dbUser};password={dbPassword};database={dbName};pooling=true";

            options.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<Industry>()
            //     .HasMany(c => c.Companies)
            //     .WithOne(c => c.Industry)
            //     .HasForeignKey(c => c.IndustryId)
            //     .IsRequired(false);

            // modelBuilder.Entity<Member>()
            //     .HasMany(m => m.Groups)
            //     .WithMany(g => g.Members)
            //     .UsingEntity<MemberGroup>(
            //         mg => mg.HasOne(g => g.Group).WithMany().HasForeignKey(g => g.GroupId),
            //         mg => mg.HasOne(m => m.Member).WithMany().HasForeignKey(m => m.MemberId),
            //         mg =>
            //         {
            //             mg.HasKey(m => new { m.GroupId, m.MemberId });
            //         }
            //     );

            // modelBuilder.Entity<GroupTaxonomy>()
            //     .HasKey(m => new { m.GroupId, m.TaxonomyId });


            // modelBuilder.Entity<MemberTaxonomy>()
            //     .HasKey(m => new { m.MemberId, m.TaxonomyId });


            // modelBuilder.Entity<Project>()
            //     .HasMany(p => p.Groups)
            //     .WithOne(g => g.Project)
            //     .HasForeignKey(g => g.ProjectId);

            // base.OnModelCreating(modelBuilder);

        }
    }
}
