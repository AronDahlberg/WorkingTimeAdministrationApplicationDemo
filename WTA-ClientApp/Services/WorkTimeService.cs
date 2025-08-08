using Blazored.LocalStorage;
using System.Net;
using WTA_ClientApp.Common;
using WTA_ClientApp.Services;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public class WorkTimeService(IClient client, ILocalStorageService localStorage) : BaseHttpService(localStorage, client), IWorkTimeService
    {
        public async Task<ServiceResult<object>> AddWorkEntryAsync(AddWorkEntryDto dto)
        {
            try
            {
                await GetBearerToken();

                await client.RegisterWorkEntryAsync(dto);

                return new ServiceResult<object>
                {
                    IsSuccess = true,
                    Data = null,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ApiException ex)
            {
                var code = (HttpStatusCode)ex.StatusCode;

                var serverText = ex.Response?.Trim();

                if (string.IsNullOrWhiteSpace(serverText) && !string.IsNullOrWhiteSpace(ex.Message))
                {
                    serverText = ex.Message;
                }

                string message = code switch
                {
                    HttpStatusCode.BadRequest => !string.IsNullOrEmpty(serverText)
                        ? $"Bad request: {serverText}"
                        : "Bad request.",
                    HttpStatusCode.Unauthorized => "You are not authenticated. Please sign in and try again.",
                    HttpStatusCode.Forbidden => "You do not have permission to perform this action.",
                    HttpStatusCode.NotFound => "Requested resource not found.",
                    HttpStatusCode.Conflict => !string.IsNullOrEmpty(serverText)
                        ? $"Conflict: {serverText}"
                        : "Conflict.",
                    _ when !string.IsNullOrEmpty(serverText) => $"Server returned {(int)code}: {serverText}",
                    _ => $"Server returned {(int)code} ({code})."
                };

                return new ServiceResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = message,
                    StatusCode = code
                };
            }
            catch (HttpRequestException)
            {
                return new ServiceResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "Cannot reach the server. Please check your network connection."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
        public async Task<ServiceResult<ICollection<WorkEntryDto>>> GetWorkEntriesAsync(int employeeId)
        {
            try
            {
                var entries = await client.GetWorkEntriesAsync(employeeId);

                if (entries == null)
                {
                    return new ServiceResult<ICollection<WorkEntryDto>>
                    {
                        IsSuccess = false,
                        ErrorMessage = "Server returned no data.",
                    };
                }

                return new ServiceResult<ICollection<WorkEntryDto>>
                {
                    IsSuccess = true,
                    Data = entries,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ApiException ex)
            {
                var code = (HttpStatusCode)ex.StatusCode;
                var serverText = ex.Response?.Trim();

                if (string.IsNullOrEmpty(serverText) && !string.IsNullOrEmpty(ex.Message))
                {
                    serverText = ex.Message;
                }

                string message = code switch
                {
                    HttpStatusCode.BadRequest => !string.IsNullOrEmpty(serverText)
                        ? $"Bad request: {serverText}"
                        : "Bad request.",
                    HttpStatusCode.Unauthorized => "You are not authenticated. Please sign in and try again.",
                    HttpStatusCode.Forbidden => "You do not have permission to view these work entries.",
                    HttpStatusCode.NotFound => "No work entries found for this employee.",
                    _ when !string.IsNullOrEmpty(serverText) => $"Server returned {(int)code}: {serverText}",
                    _ => $"Server returned {(int)code} ({code})."
                };

                return new ServiceResult<ICollection<WorkEntryDto>>
                {
                    IsSuccess = false,
                    ErrorMessage = message,
                    StatusCode = code
                };
            }
            catch (HttpRequestException)
            {
                return new ServiceResult<ICollection<WorkEntryDto>>
                {
                    IsSuccess = false,
                    ErrorMessage = "Cannot reach the server. Please check your network connection."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<WorkEntryDto>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }
    }
}
