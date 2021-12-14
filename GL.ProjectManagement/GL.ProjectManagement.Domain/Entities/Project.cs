using GL.ProjectManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Project : EntityBase, ITraceableEntity
    {      
        public string Name { get; set; }
        public string Detail { get; set; }

        public DateTime CreatedOn { get; set; } 

        // Navigations
        [JsonIgnore]
        public List<Task> Task { get; set; }
        
    }
}
