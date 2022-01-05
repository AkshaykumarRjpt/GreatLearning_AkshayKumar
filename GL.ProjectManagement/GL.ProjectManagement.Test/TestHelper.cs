using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GL.ProjectManagement.Test
{
    public static class TestHelper
    {
        public static dynamic GetBearerToken()
        {
            dynamic data = new ExpandoObject();
            data.sub = "Akshay@GL.com";
            data.extension_UserRole = "User";
            data.extension_UserType = "User";
            return data;
        }
    }
}
