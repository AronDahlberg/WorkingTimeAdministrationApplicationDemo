using WTA_ClientApp.Common;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public interface IWorkTimeService
    {
        Task<ServiceResult<object>> AddWorkEntryAsync(AddWorkEntryDto dto);
    }
}
