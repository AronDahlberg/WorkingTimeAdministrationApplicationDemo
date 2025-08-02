using WTA_Api.Data;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public class WorkEntryService(IWorkEntryRepository workEntryRepository) : IWorkEntryService
    {
        private readonly IWorkEntryRepository workEntryRepository = workEntryRepository;

        public async Task AddWorkEntryAsync(WorkEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Work entry cannot be null.");
            }
            await workEntryRepository.AddWorkEntryAsync(entry);
        }
    }
}
