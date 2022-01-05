using GL.ProjectManagement.API;
using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace GL.ProjectManagement.Test
{
    public class UserControllerTest 
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UserControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task UserControllerOnGETShouldReturnAllUsersInDb()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 1, FirstName = "Akshay", LastName = "Kumar", Password = "test", Email = Email.Address("Akshay@GL.com") });
                dbContext.Users.Add(new User { Id = 2, FirstName = "TestUser", LastName = "Test", Password = "test", Email = Email.Address("user@GL.com") });
                dbContext.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/User");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"firstName\":\"Akshay\",\"lastName\":\"Kumar\",\"email\":{\"emailAddress\":\"Akshay@GL.com\"},\"password\":\"\",\"id\":1}", responseString);
            Assert.Contains("{\"firstName\":\"TestUser\",\"lastName\":\"Test\",\"email\":{\"emailAddress\":\"user@GL.com\"},\"password\":\"\",\"id\":2}", responseString);
        }
    }
}
