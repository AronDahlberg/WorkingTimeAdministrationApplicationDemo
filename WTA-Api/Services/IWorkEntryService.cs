using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public interface IWorkEntryService
    {
        Task AddWorkEntryAsync(WorkEntryDto entry);
    }
}
