
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Utilities;


namespace PayRollSystem.Domain.IServices
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Login(LoginDTO model);
        Task<Response<string>> Register(RegisterDTO user, string Role);
        Task<Response<string>> RefreshToken();
        public Task<object> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPasswordAsync(UpdatePasswordDTO resetPasswordDTO);
        public Task<object> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
