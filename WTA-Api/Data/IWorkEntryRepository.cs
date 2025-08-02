using WTA_Api.Models;

namespace WTA_Api.Data
{
    public interface IWorkEntryRepository
    {
        Task<WorkEntry?> GetWorkEntryByIdAsync(int workEntryId);
        Task AddWorkEntryAsync(WorkEntry workEntry);
        Task UpdateWorkEntryAsync(WorkEntry workEntry);
        Task DeleteWorkEntryAsync(int workEntryId);
        Task<List<WorkEntry>> GetWorkEntriesByEmployeeIdAsync(int employeeId);
    }
}
