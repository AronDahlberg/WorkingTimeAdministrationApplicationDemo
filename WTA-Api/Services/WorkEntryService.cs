using WTA_Api.Data;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public class WorkEntryService(IWorkEntryRepository workEntryRepository, IEmployeeRepository employeeRepository) : IWorkEntryService
    {
        private readonly IWorkEntryRepository workEntryRepository = workEntryRepository;
        private readonly IEmployeeRepository employeeRepository = employeeRepository;

        public async Task AddWorkEntryAsync(AddWorkEntryDto entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Work entry cannot be null.");
            }

            var employee = await employeeRepository.GetEmployeeByIdAsync(entry.EmployeeId) ?? throw new KeyNotFoundException("Could not find employee to add work entry to.");

            decimal totalWage = decimal.Round(employee.HourlyWage * ((decimal)Math.Ceiling(entry.Duration.TotalMinutes / 5.0) * 5m / 60m), 2, MidpointRounding.AwayFromZero);

            var workEntry = new WorkEntry
            {
                EmployeeId = entry.EmployeeId,
                StartDateTime = entry.StartDateTime,
                Duration = entry.Duration,
                TotalWage = totalWage
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

        public async Task<List<WorkEntryDto>> GetWorkEntriesByEmployeeIdAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Invalid employee ID.", nameof(employeeId));
            }
            var workEntries = await workEntryRepository.GetWorkEntriesByEmployeeIdAsync(employeeId);
            return [.. workEntries.Select(we => new WorkEntryDto
            {
                WorkEntryId = we.WorkEntryId,
                EmployeeId = we.EmployeeId,
                StartDateTime = we.StartDateTime,
                Duration = we.Duration,
                TotalWage = we.TotalWage
            })];
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
