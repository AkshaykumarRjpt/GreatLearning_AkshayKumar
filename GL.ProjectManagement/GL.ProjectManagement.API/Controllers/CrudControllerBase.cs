
using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
//using GL.ProjectManagement.Domain.Repositories;
using GL.ProjectMangement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudControllerBase<T, TRepo> : ControllerBase 
        where T :EntityBase
        where TRepo : IRepository<T>
        
    {       
        private readonly IRepository<T> repository;

        protected DbSet<T> _dbSet { get; set; }
        public CrudControllerBase(IRepository<T> repository)
        {           
            this.repository = repository;
        }

        
        [HttpGet]
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                
                return repository.All();
                
            });
        }

        [HttpGet("{id}")]
        public virtual async Task<T> GetAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {

                return repository.Get(id.ToString());

            });
        }

        [HttpPost]
        public virtual async Task<T> CreateAsync(T entity)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {

                return repository.Add(entity);

            });
        }

        [HttpPut()]
        public virtual async Task<IActionResult> UpdateAsync(T entity)
        {
            if (repository.Update(entity) != null)
                return Ok($"Update successful for id: {entity.Id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            if (repository.Delete(id.ToString()) != null)
                return Ok($"id {id} Delete successful");
            return NoContent();
        }

    }
}
