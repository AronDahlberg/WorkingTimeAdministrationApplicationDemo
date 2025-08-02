using WTA_Api.Models;

namespace WTA_Api.Data
{
    public class WorkEntryRepository : IWorkEntryRepository
    {
        public Task AddWorkEntryAsync(WorkEntry workEntry)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkEntryAsync(int workEntryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkEntry>> GetWorkEntriesByEmployeeIdAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkEntry?> GetWorkEntryByIdAsync(int workEntryId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkEntryAsync(WorkEntry workEntry)
        {
            throw new NotImplementedException();
        }
    }
}
