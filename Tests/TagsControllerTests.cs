using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API;
using Application.Resources.Tag;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class TagsControllerTests : BaseIntegrationTest
    {
        [Fact]
        public async Task GetAllAsync_When_There_Is_No_Tags_EmptyResponse()
        {
            // Arrange
            await RemoveEntries();
            await AuthenticateUserAsync();
            
            // Act
            var response = await client.GetAsync("/api/tags");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<TagResource>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_When_There_Are_Tasks_ResourcesList()
        {
            await RemoveEntries();
            await AuthenticateUserAsync();
            
        }
    }
}