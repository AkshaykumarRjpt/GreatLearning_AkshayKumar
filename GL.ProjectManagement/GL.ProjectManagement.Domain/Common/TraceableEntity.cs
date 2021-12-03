using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Common
{
    public interface ITraceableEntity
    {
        public DateTime CreatedOn { get; set; }
    }
}
