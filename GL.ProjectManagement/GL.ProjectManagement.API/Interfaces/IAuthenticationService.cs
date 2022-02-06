using GL.ProjectManagement.API.DTO;

namespace GL.ProjectManagement.API.Interfaces
{
    public interface IAuthenticationService
    {
        LoginResponseDTO Authenticate(LoginCredentials loginCredentials);
    }
}
