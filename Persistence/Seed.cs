using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Contexts;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DatabaseContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new()
                    {
                        Id = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
                        FirstName = "Maciek",
                        LastName = "Grzela",
                        Email = "testemail@mail.com",
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

            // if(!context.TodoSets.Any()){
            //     var todoSets = new List<TodoSet>
            //     {
            //         new()
            //         {
            //             Id = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64"),
            //             OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
            //             Name = "Zakupy wieczorem"
            //         }
            //     };
            //
            //     await context.TodoSets.AddRangeAsync(todoSets);
            //     await context.SaveChangesAsync();
            // }
            //
            // if(!context.Tags.Any()){
            //     var tag = new List<Tag>
            //     {
            //         new() 
            //         {
            //             Id = Guid.Parse("b74c4c7f-d5bc-45b0-b39b-5761548f6569"),
            //             OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
            //             Name = "Zakupy"
            //         },
            //         new()
            //         {
            //             Id = Guid.Parse("626638fc-a1e9-4e5f-bedc-c2faf170df00"),
            //             OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
            //             Name = "Produktywność"
            //         },
            //         new()
            //         {
            //             Id = Guid.Parse("fe9d074a-9871-4aa5-940d-06976a254b18"),
            //             OwnerId = Guid.Parse("e837de4f-4578-47e5-80b3-c01095a4c893").ToString(),
            //             Name = "Praca"
            //         }
            //     };
            //     await context.Tags.AddRangeAsync(tag);
            //     await context.SaveChangesAsync();
            // }
            //
            // if(!context.TodoSetTags.Any()){
            //     var todoSetTags = new List<TodoSetTags>
            //     {
            //         new()
            //         {
            //             TagId = Guid.Parse("b74c4c7f-d5bc-45b0-b39b-5761548f6569"),
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         },
            //         new()
            //         {
            //             TagId = Guid.Parse("626638fc-a1e9-4e5f-bedc-c2faf170df00"),
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         },
            //         new()
            //         {
            //             TagId = Guid.Parse("fe9d074a-9871-4aa5-940d-06976a254b18"),
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         }
            //     };
            //
            //     await context.TodoSetTags.AddRangeAsync(todoSetTags);
            //     await context.SaveChangesAsync();
            // }
            //
            //
            // if(!context.Todos.Any()){
            //     var todos = new List<Todo>
            //     {
            //         new()
            //         {
            //             Id = Guid.NewGuid(),
            //             Name = "2 kilogramy jabłek",
            //             Checked = false,
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         },
            //         new()
            //         {
            //             Id = Guid.NewGuid(),
            //             Name = "3 marchewki",
            //             Checked = false,
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         },
            //         new()
            //         {
            //             Id = Guid.NewGuid(),
            //             Name = "Jedna pomarańcza",
            //             Checked = false,
            //             TodoSetId = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64")
            //         }
            //     };
            //
            //     await context.Todos.AddRangeAsync(todos);
            //     await context.SaveChangesAsync();
            // }
        }
    }
}