using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Persistence.Contexts;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedData(DatabaseContext context) {

            if(!context.TaskSets.Any()){
                var taskSets = new List<TaskSet>
                {
                    new TaskSet
                    {
                        Id = Guid.Parse("da8dd78e-fecc-4461-92df-18dd13c07d64"),
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
                        Name = "Zakupy"
                    },
                    new Tag()
                    {
                        Id = Guid.Parse("626638fc-a1e9-4e5f-bedc-c2faf170df00"),
                        Name = "Produktywność"
                    },
                    new Tag()
                    {
                        Id = Guid.Parse("fe9d074a-9871-4aa5-940d-06976a254b18"),
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