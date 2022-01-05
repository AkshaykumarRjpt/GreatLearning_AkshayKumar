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

        [Fact]
        public async System.Threading.Tasks.Task UserControllerOnGetByIDCallShouldReturnUserById()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 3, FirstName = "Akshay", LastName = "Kumar", Password = "test", Email = Email.Address("Akshay@GL.com") });
                dbContext.Users.Add(new User { Id = 4, FirstName = "TestUser", LastName = "Test", Password = "test", Email = Email.Address("user@GL.com") });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
           new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Akshay@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);

            // Act
            var response = await _client.GetAsync("/api/User/4");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"firstName\":\"TestUser\",\"lastName\":\"Test\",\"email\":{\"emailAddress\":\"user@GL.com\"},\"password\":\"\",\"id\":4}", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task UserControllerOnDeleteCallShouldDeleteTaskWhenUserExists()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 4, FirstName = "Akshay", LastName = "Kumar", Password = "test", Email = Email.Address("Akshay@GL.com") });
                dbContext.Users.Add(new User { Id = 5, FirstName = "TestUser", LastName = "Test", Password = "test", Email = Email.Address("user@GL.com") });
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
            new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Akshay@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);
            
            //Act
            var response = await _client.DeleteAsync("/api/User/4");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("id 4 Delete successful", responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task UserControllerOnPostCreateUser()
        {
            // Arrange
            var body = new
            {               
                FirstName = "Akshay",
                LastName = "Kumar",
                Email = Email.Address("Akshay.kumar@gl.com"),
                Password = "Test"
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 6, FirstName = "Akshay", LastName = "Kumar", Password = "test", Email = Email.Address("Akshay@GL.com") });               
                dbContext.SaveChanges();
            }
            var login = await _client.PostAsync("api/login",
            new StringContent(JsonConvert.SerializeObject(new LoginCredentials { Email = "Akshay@GL.com", Password = "test" }), Encoding.Default, "application/json"));
            _client.SetToken("Bearer", login.Content.ReadAsStringAsync().Result);


            // Act
            var response = await _client.PostAsync("/api/User",
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }

        [Fact]
        public async System.Threading.Tasks.Task UserControllerOnPutShouldUpdateUser()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>();
                dbContext.Users.Add(new User { Id = 7, FirstName = "Test", LastName = "Test", Password = "test", Email = Email.Address("Test@GL.com") });
                
                dbContext.SaveChanges();
            }
            var body = new
            {
                Id = 7, 
                FirstName = "Akshay",
                LastName = "Kumar",
                Email = Email.Address("Akshay.kumar@gl.com"),
                Password = "Test"
            };

            // Act
            var response = await _client.PutAsync("/api/User", 
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Update successful for id: 7", responseString);
        }
    }
}
