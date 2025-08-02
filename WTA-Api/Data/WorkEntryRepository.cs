using WTA_Api.Models;

namespace WTA_Api.Data
{
    public class WorkEntryRepository(ApplicationDbContext context) : IWorkEntryRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task AddWorkEntryAsync(WorkEntry workEntry)
        {
            if (workEntry == null)
            {
                throw new ArgumentNullException(nameof(workEntry), "Work entry cannot be null.");
            }
            context.WorkEntries.Add(workEntry);
            await context.SaveChangesAsync();
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
