using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.TestingSystemDBContext
{
    public class TestingSystemDbContext : IdentityDbContext<IdentityUser>, ITestingSystemDbContext
    {
        public TestingSystemDbContext(DbContextOptions<TestingSystemDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TestingSystemDbContext).Assembly);
        }
        //public virtual DbSet<IdentityUser> IdentityUser { get; set; }
        public virtual DbSet<UserTests> UserTests { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        public class UserTestsEntityTypeConfiguration : IEntityTypeConfiguration<UserTests>
        {
            public void Configure(EntityTypeBuilder<UserTests> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .HasOne(x => x.IdentityUser)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
                //.HasPrincipalKey(x=>x.Id);

                builder
                    .HasOne(x => x.Test)
                    .WithMany()
                    .HasForeignKey(x => x.TestId);
            }
        }
        public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
        {
            public void Configure(EntityTypeBuilder<Question> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .HasOne(x => x.Test)
                    .WithMany(x=>x.Questions)
                    .HasForeignKey(x => x.TestId);

                builder
                    .HasMany(x => x.Answers)
                    .WithOne(x=>x.Question)
                    .HasForeignKey(x => x.QuestionId);
            }
        }
    }
}
