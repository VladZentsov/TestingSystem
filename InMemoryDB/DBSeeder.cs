using AutoMapper;
using BLL;
using DAL.TestingSystemDBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace InMemoryDB
{
    public class DBSeeder
    {
        //public static DbContextOptions<TestingSystemDbContext> GetUnitTestDbOptions()
        //{
        //    var options = new DbContextOptionsBuilder<TestingSystemDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new TestingSystemDbContext(options))
        //    {
        //        SeedData(context, bu);
        //    }

        //    return options;
        //}

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static DbContextOptions<TestingSystemDbContext> GetUnitEmptyDbOptions()
        {
            var options = new DbContextOptionsBuilder<TestingSystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return options;
        }


        public static async Task SeedData(TestingSystemDbContext context)
        {
            var userStore = new UserStore<IdentityUser>(context);
            var roleStore = new RoleStore<IdentityRole>(context);

             await roleStore.CreateAsync(new IdentityRole { Name = "user", NormalizedName = "user" });

            await userStore.CreateAsync(IdentityUsers.ElementAt(0));
            await userStore.AddToRoleAsync(IdentityUsers.ElementAt(0), "user");


            //builder.Entity<IdentityUser>().HasData(IdentityUsers);
            //context.IdentityUser.AddRange(IdentityUsers);
            await context.SaveChangesAsync();
        }

        public static List<IdentityUser> IdentityUsers
        {
            get
            {
                List<IdentityUser> identityUsers = new List<IdentityUser>()
                {
                    new IdentityUser() {Id = "1", UserName = "MaxMartynov", Email = "MaxM@gmail.com" }
                };

                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(identityUsers.ElementAt(0), "ABCD1234");
                identityUsers.ElementAt(0).PasswordHash = hashed;

                return identityUsers;
            }
        }
    }
}
