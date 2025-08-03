using WTA_Api.Data;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public class WorkEntryService(IWorkEntryRepository workEntryRepository) : IWorkEntryService
    {
        private readonly IWorkEntryRepository workEntryRepository = workEntryRepository;

        public async Task AddWorkEntryAsync(AddWorkEntryDto entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Work entry cannot be null.");
            }

            var workEntry = new WorkEntry
            {
                EmployeeId = entry.EmployeeId,
                StartDateTime = entry.StartDateTime,
                Duration = entry.Duration,
                TotalWage = entry.TotalWage
            };

            await workEntryRepository.AddWorkEntryAsync(workEntry);
        }

        public async Task DeleteWorkEntryAsync(int entryId)
        {
            if (entryId <= 0)
            {
                throw new ArgumentException("Invalid work entry ID.", nameof(entryId));
            }

            await workEntryRepository.DeleteWorkEntryAsync(entryId);
        }

        public async Task UpdateWorkEntryAsync(WorkEntryDto entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Work entry cannot be null.");
            }
            if (entry.WorkEntryId <= 0)
            {
                throw new ArgumentException("Invalid work entry ID.", nameof(entry.WorkEntryId));
            }

            WorkEntry workEntry = await workEntryRepository.GetWorkEntryByIdAsync(entry.WorkEntryId)
                                    ?? throw new KeyNotFoundException($"Work entry with ID {entry.WorkEntryId} not found.");

            workEntry.StartDateTime = entry.StartDateTime;
            workEntry.Duration = entry.Duration;
            workEntry.TotalWage = entry.TotalWage;
            workEntry.EmployeeId = entry.EmployeeId;

            await workEntryRepository.UpdateWorkEntryAsync(workEntry);
        }
    }
}
