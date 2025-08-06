using Blazored.LocalStorage;
using System.Net;
using WTA_ClientApp.Common;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public class UserService(IClient client, ILocalStorageService localStorage) : BaseHttpService(localStorage, client), IUserService
    {
        public async Task<ServiceResult<UserDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                await GetBearerToken();

                var dto = await client.GetUserAsync(userId);

                return new ServiceResult<UserDto>
                {
                    IsSuccess = true,
                    Data = dto,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ApiException ex)
            {
                var code = (HttpStatusCode)ex.StatusCode;

                var serverText = ex.Response?.Trim();

                string message = code switch
                {
                    HttpStatusCode.Unauthorized => "You are not logged in. Please sign in and try again.",
                    HttpStatusCode.Forbidden => "You do not have permission to view this user.",
                    HttpStatusCode.NotFound => "This user does not exist.",
                    _ when !string.IsNullOrEmpty(serverText)
                        => $"Server returned {(int)code}: {serverText}",
                    _ => $"Server returned {(int)code} {code}"
                };

                return new ServiceResult<UserDto>
                {
                    IsSuccess = false,
                    ErrorMessage = message,
                    StatusCode = code
                };
            }
            catch (HttpRequestException)
            {
                return new ServiceResult<UserDto>
                {
                    IsSuccess = false,
                    ErrorMessage = "Cannot reach the server. Please check your network connection."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDto>
                {
                    IsSuccess = false,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
    }
}
