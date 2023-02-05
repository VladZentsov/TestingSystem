using AutoMapper;
using BLL;
using DAL.Entities;
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

            await userStore.CreateAsync(IdentityUsers.ElementAt(1));
            await userStore.AddToRoleAsync(IdentityUsers.ElementAt(1), "user");

            context.Tests.AddRange(Tests);
            context.Questions.AddRange(Questions);
            context.UserTests.AddRange(UserTests);
            context.Answers.AddRange(Answers);


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
                    new IdentityUser() {Id = "1", UserName = "MaxMartynov", Email = "MaxM@gmail.com" },
                    new IdentityUser() {Id = "2", UserName = "VladZientsov", Email = "vladzentso07@gmail.com" }
                };

                var password = new PasswordHasher<IdentityUser>();

                var hashed = password.HashPassword(identityUsers.ElementAt(0), "ABCD1234");
                identityUsers.ElementAt(0).PasswordHash = hashed;

                hashed = password.HashPassword(identityUsers.ElementAt(1), "Agyr6389");
                identityUsers.ElementAt(1).PasswordHash = hashed;

                return identityUsers;
            }
        }

        public static List<Test> Tests
        {
            get
            {
                return new List<Test>()
                {
                    new Test(){ Id = "1", Name = "History", Description = "History of ancient rome. 60 minutes"},
                    new Test(){ Id = "2", Name = "Biology", Description = "10 - 11 class. 120 minutes"},
                    new Test(){ Id = "3", Name = "Math", Description = "10 - 11 class. 160 minutes"},
                };
            }
        }

        public static List<UserTests> UserTests
        {
            get
            {
                return new List<UserTests>()
                {
                    new UserTests(){Id = "1", UserId = "1", TestId = "2" },
                    new UserTests(){Id = "2", UserId = "1", TestId = "3" },

                    new UserTests(){Id = "3", UserId = "2", TestId = "2" },
                    new UserTests(){Id = "4", UserId = "2", TestId = "3" },
                    new UserTests(){Id = "5", UserId = "2", TestId = "1" },
                };
            }
        }

        public static List<Question> Questions
        {
            get
            {
                return new List<Question>()
                {
                    new Question(){Id = "1", TestId = "1", Number = 1, QuestionText = "How to characterize the difference between patricians and plebeians?"},
                    new Question(){Id = "2", TestId = "1", Number = 2, QuestionText = "What word was spoken in the Senate that prohibited the passing of a law?" },
                    new Question(){Id = "3", TestId = "1", Number = 3, QuestionText = "Name the main military unit of the Roman army." },

                    new Question(){Id = "4", TestId = "2", Number = 1, QuestionText = "What is fauna?" },
                    new Question(){Id = "5", TestId = "2", Number = 2, QuestionText = "Which of the following definitions corresponds to the concept of \"population\"?" },
                    new Question(){Id = "6", TestId = "2", Number = 3, QuestionText = "Which of the following definitions corresponds to the concept of \"area\"? " },

                    new Question(){Id = "7", TestId = "3", Number = 1, QuestionText = "Simplify the expression: -4m + 9n - 7m - 2n" },
                    new Question(){Id = "8", TestId = "3", Number = 2, QuestionText = "Solve the equation: 10y - 13.5 = 2y - 37.5" },
                    new Question(){Id = "9", TestId = "3", Number = 3, QuestionText = "Express y in terms of x in the expression: -5x + y = -17" }
                };
            }
        }

        public static List<Answer> Answers
        {
            get
            {
                return new List<Answer>()
                {
                    new Answer(){Id = "1", QuestionId = "1", AnswerText = "noble - ignorant", IsAnswerCorrect = true},
                    new Answer(){Id = "2", QuestionId = "1", AnswerText = "warriors - plowmen", IsAnswerCorrect = false},
                    new Answer(){Id = "3", QuestionId = "1", AnswerText = "kings - subjects", IsAnswerCorrect = false},
                    new Answer(){Id = "4", QuestionId = "1", AnswerText = "slave owners - slaves", IsAnswerCorrect = false},

                    new Answer(){Id = "6", QuestionId = "2", AnswerText = "illegally", IsAnswerCorrect = false},
                    new Answer(){Id = "7", QuestionId = "2", AnswerText = "anathema", IsAnswerCorrect = false},
                    new Answer(){Id = "8", QuestionId = "2", AnswerText = "interdict", IsAnswerCorrect = false},
                    new Answer(){Id = "9", QuestionId = "2", AnswerText = "veto", IsAnswerCorrect = true},

                    new Answer(){Id = "10", QuestionId = "3", AnswerText = "phalanx", IsAnswerCorrect = false},
                    new Answer(){Id = "11", QuestionId = "3", AnswerText = "legion", IsAnswerCorrect = true},
                    new Answer(){Id = "12", QuestionId = "3", AnswerText = "line", IsAnswerCorrect = false},
                    new Answer(){Id = "13", QuestionId = "3", AnswerText = "square", IsAnswerCorrect = false},

                    new Answer(){Id = "14", QuestionId = "4", AnswerText = "a historically established set of all animal species living in a certain territory or water area.", IsAnswerCorrect = true},
                    new Answer(){Id = "15", QuestionId = "4", AnswerText = "a natural complex in which all the main components (relief, climate, water, soil, vegetation, wildlife) are in complex interaction, forming a single system that is homogeneous in terms of development.", IsAnswerCorrect = false},
                    new Answer(){Id = "16", QuestionId = "4", AnswerText = "a set of animal individuals that is part of a community and is characterized by species composition, number of individuals, structural organization and typology of the constituent groups. ", IsAnswerCorrect = false},
                    new Answer(){Id = "17", QuestionId = "4", AnswerText = "a set of homogeneous natural phenomena over a known extent of the earth's surface, which has its own specific interactions of these constituent components and a certain type of matter and energy exchange", IsAnswerCorrect = false},

                    new Answer(){Id = "18", QuestionId = "5", AnswerText = "a historically established set of all animal species living in a certain territory or water area.", IsAnswerCorrect = false},
                    new Answer(){Id = "19", QuestionId = "5", AnswerText = "a natural complex in which all the main components (relief, climate, water, soil, vegetation, wildlife) are in complex interaction, forming a single system that is homogeneous in terms of development.", IsAnswerCorrect = false},
                    new Answer(){Id = "20", QuestionId = "5", AnswerText = "a set of animal individuals that is part of a community and is characterized by species composition, number of individuals, structural organization and typology of the constituent groups.", IsAnswerCorrect = true},
                    new Answer(){Id = "21", QuestionId = "5", AnswerText = "a set of homogeneous natural phenomena over a known extent of the earth's surface, which has its own specific interactions of these constituent components and a certain type of matter and energy exchange ", IsAnswerCorrect = false},

                    new Answer(){Id = "22", QuestionId = "6", AnswerText = "part of the earth's surface within which a given taxon (species, genus, etc.) is distributed and goes through a full cycle of its development.", IsAnswerCorrect = true},
                    new Answer(){Id = "23", QuestionId = "6", AnswerText = "a piece of land or a body of water occupied by a part of a population of individuals of the same species and possessing all the conditions necessary for their existence (climate, topography, soil, etc.).", IsAnswerCorrect = false},
                    new Answer(){Id = "24", QuestionId = "6", AnswerText = "a section of space characterized by a set of conditions (relief, climate, food, shelters, etc.) necessary for the existence of a given species of animal.", IsAnswerCorrect = false},
                    new Answer(){Id = "25", QuestionId = "6", AnswerText = "a set of homogeneous natural phenomena over a known extent of the earth's surface, which has its own specific interactions of these constituent components and a certain type of exchange of matter and energy.", IsAnswerCorrect = false},

                    new Answer(){Id = "26", QuestionId = "7", AnswerText = "-111m + 7n", IsAnswerCorrect = false},
                    new Answer(){Id = "27", QuestionId = "7", AnswerText = "-11m + 7n", IsAnswerCorrect = true},
                    new Answer(){Id = "28", QuestionId = "7", AnswerText = "21m + 4n", IsAnswerCorrect = false},
                    new Answer(){Id = "29", QuestionId = "7", AnswerText = "-16m + 6n", IsAnswerCorrect = false},

                    new Answer(){Id = "30", QuestionId = "8", AnswerText = "10", IsAnswerCorrect = false},
                    new Answer(){Id = "31", QuestionId = "8", AnswerText = "-3", IsAnswerCorrect = true},
                    new Answer(){Id = "32", QuestionId = "8", AnswerText = "-4", IsAnswerCorrect = false},
                    new Answer(){Id = "33", QuestionId = "8", AnswerText = "12", IsAnswerCorrect = false},

                    new Answer(){Id = "34", QuestionId = "9", AnswerText = "y = -5x + 17", IsAnswerCorrect = false},
                    new Answer(){Id = "35", QuestionId = "9", AnswerText = "y = -17 + 5x", IsAnswerCorrect = true},
                    new Answer(){Id = "36", QuestionId = "9", AnswerText = "y = -10x + 17", IsAnswerCorrect = false},
                    new Answer(){Id = "37", QuestionId = "9", AnswerText = "y = -55x + 170", IsAnswerCorrect = false},
                };
            }
        }
    }
}
