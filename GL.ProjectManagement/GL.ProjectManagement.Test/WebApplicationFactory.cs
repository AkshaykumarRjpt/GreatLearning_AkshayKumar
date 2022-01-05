using GL.ProjectManagement.API;
using GL.ProjectManagement.Domain.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL.ProjectManagement.Test
{
    public class WebApplicationFactory
    {
        public class WebAppFactory<T> : WebApplicationFactory<Startup>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    var dbContext = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProjectManagementDBContext>));

                    if (dbContext != null)
                        services.Remove(dbContext);

                    var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                                 .BuildServiceProvider();

                    services.AddDbContext<ProjectManagementDBContext>(options =>
                    {
                        options.UseInMemoryDatabase("ProjectmanagementDB");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    var serviceprovider = services.BuildServiceProvider();


                    using (var scope = serviceprovider.CreateScope())
                    {
                        using (var appContext = scope.ServiceProvider.GetRequiredService<ProjectManagementDBContext>())
                        {
                            try
                            {
                                appContext.Database.EnsureCreated();
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }
                    }
                });
            }

        }
    }
}
