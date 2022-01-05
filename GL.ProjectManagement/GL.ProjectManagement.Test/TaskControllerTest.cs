using GL.ProjectManagement.API;
using GL.ProjectManagement.API.DTO;
using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Enums;
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
    public class TaskControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public TaskControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskControllerOnGetShouldReturnAllTasksInDb()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Tasks.Add(new Task { Id = 111, Detail = "detail1", Status = Status.New, AssignedToUserId = 1, ProjectId = 1 });
                dbContext.Tasks.Add(new Task { Id = 2, Detail = "detail2", Status = Status.New, AssignedToUserId = 2, ProjectId = 2 });
                dbContext.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Task");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"projectId\":1,\"status\":1,\"assignedToUserId\":1,\"detail\":\"detail1\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":111}", responseString);
            Assert.Contains("{\"projectId\":2,\"status\":1,\"assignedToUserId\":2,\"detail\":\"detail2\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":2}", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskControllerOnGetByIDShouldReturnTaskById()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 76, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.Tasks.Add(new Task { Id = 3, Detail = "detail1", Status = Status.New, AssignedToUserId = 1, ProjectId = 1 });
                dbContext.Tasks.Add(new Task { Id = 4, Detail = "detail2", Status = Status.New, AssignedToUserId = 2, ProjectId = 2 });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
           new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.GetAsync("/api/Task/3");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"projectId\":1,\"status\":1,\"assignedToUserId\":1,\"detail\":\"detail1\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":3}", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskControllerOnDeleteCallShouldDeleteTaskWhenTaskExists()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 11, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.Tasks.Add(new Task { Id = 5, Detail = "detail1", Status = Status.New, AssignedToUserId = 1, ProjectId = 1 });
                dbContext.Tasks.Add(new Task { Id = 6, Detail = "detail2", Status = Status.New, AssignedToUserId = 2, ProjectId = 2 });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
           new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.DeleteAsync("/api/Task/5");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("id 5 Delete successful", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskControllerOnPostCallShouldCreateTask()
        {
            // Arrange
            var body = new
            {
                projectId = 1,
                assignedToUserId = 1,
                detail = "detail5"
            };

            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 12, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });                
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
           new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.PostAsync("/api/Task", 
                new StringContent(JsonConvert.SerializeObject(body),
                Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"projectId\":1,\"status\":0,\"assignedToUserId\":1,\"detail\":\"detail5\",\"createdOn\":\"0001-01-01T00:00:00\",\"id\":1", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskControllerOnPutCallShouldUpdateTask()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Tasks.Add(new Task { Id = 7, Detail = "detail1", Status = Status.New, AssignedToUserId = 1, ProjectId = 1 });
                dbContext.Users.Add(new User { Id = 113, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
          new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Test@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);
            var body = new
            {
                id = 7,
                status = 1,
                projectId = 1,
                assignedToUserId = 1,
                detail = "detail5"
            };

            // Act
            var response = await _client.PutAsync("/api/Task", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Update successful for id: 7", responseString);
        }
    }
}
