using GL.ProjectManagement.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class User: EntityBase
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        
        public string Password { get; set; }

        //Navigations

        [JsonIgnore]
        public IList<Task> Tasks { get; set; }
    }
}
