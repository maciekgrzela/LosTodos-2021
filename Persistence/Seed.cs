using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Contexts;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedData(DatabaseContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        FirstName = "Maciek",
                        LastName = "Grzela",
                        Email = "maciekgrzela45@gmail.com",
                        UserName = "mgrzela"
                    }
                };

                if(!roleManager.Roles.Any()){
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("RegularUser"));
                }

                foreach(var user in users) {
                    await userManager.CreateAsync(user, "zaq1@WSX");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if(!context.TaskSets.Any()){
                var taskSets = new List<TaskSet>
                {
                    new TaskSet
                    {
                        Id = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64"),
                        OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        Name = "Zakupy wieczorem"
                    }
                };

                await context.TaskSets.AddRangeAsync(taskSets);
                await context.SaveChangesAsync();
            }

            if(!context.Tags.Any()){
                var tag = new List<Tag>
                {
                    new Tag() 
                    {
                        Id = Guid.Parse("b74c4c7f-d5bc-45b0-b39b-5761548f6569"),
                        OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        Name = "Zakupy"
                    },
                    new Tag()
                    {
                        Id = Guid.Parse("626638fc-a1e9-4e5f-bedc-c2faf170df00"),
                        OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        Name = "Produktywność"
                    },
                    new Tag()
                    {
                        Id = Guid.Parse("fe9d074a-9871-4aa5-940d-06976a254b18"),
                        OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        Name = "Praca"
                    }
                };
                await context.Tags.AddRangeAsync(tag);
                await context.SaveChangesAsync();
            }

            if(!context.TaskSetTags.Any()){
                var taskSetTags = new List<TaskSetTags>
                {
                    new TaskSetTags
                    {
                        TagId = Guid.Parse("b74c4c7f-d5bc-45b0-b39b-5761548f6569"),
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    },
                    new TaskSetTags
                    {
                        TagId = Guid.Parse("626638fc-a1e9-4e5f-bedc-c2faf170df00"),
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    },
                    new TaskSetTags
                    {
                        TagId = Guid.Parse("fe9d074a-9871-4aa5-940d-06976a254b18"),
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    }
                };

                await context.TaskSetTags.AddRangeAsync(taskSetTags);
                await context.SaveChangesAsync();
            }


            if(!context.Tasks.Any()){
                var tasks = new List<Domain.Models.Task>
                {
                    new Domain.Models.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "2 kilogramy jabłek",
                        Checked = false,
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    },
                    new Domain.Models.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "3 marchewki",
                        Checked = false,
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    },
                    new Domain.Models.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jedna pomarańcza",
                        Checked = false,
                        TaskSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
                    }
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}