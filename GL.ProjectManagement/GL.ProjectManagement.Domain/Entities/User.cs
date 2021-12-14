using GL.ProjectManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class User: EntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public Email Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        //Navigations

        [JsonIgnore]
        public IList<Task> Tasks { get; set; }
    }
}
