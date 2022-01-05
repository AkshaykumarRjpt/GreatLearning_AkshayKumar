using GL.ProjectManagement.API;
using GL.ProjectManagement.API.DTO;
using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace GL.ProjectManagement.Test
{
    public class ProjectControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public ProjectControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task ProjectControllerOnGetCallShouldReturnAllProjectsInDb()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Projects.Add(new Project { Id = 1, Detail = "detail1", Name = "Test Proj1" });
                dbContext.Projects.Add(new Project { Id = 2, Detail = "detail2", Name = "Test Proj2" });
                dbContext.Users.Add(new User { Id = 13, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
         new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.GetAsync("/api/Project");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"name\":\"Test Proj1\",\"detail\":\"detail1\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":1}", responseString);
            Assert.Contains("{\"name\":\"Test Proj2\",\"detail\":\"detail2\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":2}", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task ProjectControllerOnGetByIDCallShouldReturnProjectById()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 14, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.Projects.Add(new Project { Id = 33, Detail = "detail1", Name = "Test Proj1" });
                dbContext.Projects.Add(new Project { Id = 4, Detail = "detail2", Name = "Test Proj2" });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
       new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.GetAsync("/api/Project/4");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());            
            Assert.Contains("{\"name\":\"Test Proj2\",\"detail\":\"detail2\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":4}", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task ProjectControllerOnDeleteShouldDeleteTaskWhenProjectExists()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 15, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.Projects.Add(new Project { Id = 5, Detail = "detail2", Name = "Test Proj2" });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
       new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.DeleteAsync("/api/Project/5");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("id 5 Delete successful", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task ProjectControllerOnPutCallShouldCreateProject()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 16, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });                
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
       new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            var body = new
            {
                name = "test",
                detail = "detail"
            };

            // Act
            var response = await _client.PostAsync("/api/Project", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"name\":\"test\",\"detail\":\"detail\",\"createdOn\":\"0001-01-01T00:00:00", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task ProjectControllerOnPutShouldUpdateProject()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Projects.Add(new Project { Id = 67, Detail = "detail2", Name = "Test Proj2" });
                dbContext.Users.Add(new User { Id = 77, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.SaveChanges();
            };
            var login = await _client.PostAsync("api/login",
                new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);
            var body = new
            {
                id = 67,
                name = "test",
                detail = "detail"
            };

            // Act
            var response = await _client.PutAsync("/api/Project", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Update successful for id: 67", responseString);
        }
    }
}
