using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API;
using Application.Resources.User;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Tests
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient client;
        private readonly IServiceScope scope;

        protected BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            appFactory.Server.BaseAddress = new Uri("http://localhost:5000");
            scope = appFactory.Services.CreateScope();
            client = appFactory.CreateClient();
        }

        protected async Task RemoveEntries()
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DatabaseContext>();
            context.RemoveRange(context.TodoSetTags);
            context.RemoveRange(context.Todos);
            context.RemoveRange(context.Tags);
            context.RemoveRange(context.TodoSets);
            await context.SaveChangesAsync();
        }

        protected async Task AuthenticateUserAsync()
        {
            var token = await GetTokenAsync();
            client.DefaultRequestHeaders.Authorization  = AuthenticationHeaderValue.Parse($"Bearer {token}");
        }

        private async Task<string> GetTokenAsync()
        {
            var response = await client.PostAsJsonAsync("/api/users/login", new UserCredentialsResource
            {
                Email = "testemail@mail.com",
                Password = "zaq1@WSX",
            });
            
            var result = await response.Content.ReadAsAsync<LoggedUserResource>();
            return result?.Token;
        }
    }
}