using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public interface IWorkEntryService
    {
        Task AddWorkEntryAsync(AddWorkEntryDto entry);
        Task DeleteWorkEntryAsync(int entryId);
        Task UpdateWorkEntryAsync(WorkEntryDto entry);
        Task<List<WorkEntryDto>> GetWorkEntriesByEmployeeIdAsync(int employeeId);
    }
}
