using GL.ProjectManagement.API.DTO;

namespace GL.ProjectManagement.API.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(LoginCredentials loginCredentials);
    }
}
