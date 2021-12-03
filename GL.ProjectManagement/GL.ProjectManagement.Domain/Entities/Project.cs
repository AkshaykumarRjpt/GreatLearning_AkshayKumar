using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
