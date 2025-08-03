using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteWorkEntryAsync(int workEntryId)
        {
            if (workEntryId <= 0)
            {
                throw new ArgumentException("Invalid work entry ID.", nameof(workEntryId));
            }
            var workEntry = context.WorkEntries.Find(workEntryId);
            if (workEntry == null)
            {
                throw new KeyNotFoundException($"Work entry with ID {workEntryId} not found.");
            }
            context.WorkEntries.Remove(workEntry);
            await context.SaveChangesAsync();
        }

        public async Task<List<WorkEntry>> GetWorkEntriesByEmployeeIdAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Invalid employee ID.", nameof(employeeId));
            }

            if(!await context.Employees.AnyAsync(e => e.EmployeeId == employeeId))
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            return await context.WorkEntries
                .Where(we => we.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<WorkEntry?> GetWorkEntryByIdAsync(int workEntryId)
        {
            if (workEntryId <= 0)
            {
                throw new ArgumentException("Invalid work entry ID.", nameof(workEntryId));
            }
            return await context.WorkEntries
                .Include(we => we.Employee)
                .FirstOrDefaultAsync(we => we.WorkEntryId == workEntryId);
        }

        public async Task UpdateWorkEntryAsync(WorkEntry workEntry)
        {
            if (workEntry == null)
            {
                throw new ArgumentNullException(nameof(workEntry), "Work entry cannot be null.");
            }
            if (workEntry.WorkEntryId <= 0)
            {
                throw new ArgumentException("Invalid work entry ID.", nameof(workEntry.WorkEntryId));
            }

            var existingWorkEntry = await context.WorkEntries.FindAsync(workEntry.WorkEntryId) ?? throw new KeyNotFoundException($"Work entry with ID {workEntry.WorkEntryId} not found.");
            
            existingWorkEntry.StartDateTime = workEntry.StartDateTime;
            existingWorkEntry.Duration = workEntry.Duration;
            existingWorkEntry.TotalWage = workEntry.TotalWage;
            existingWorkEntry.EmployeeId = workEntry.EmployeeId;

            context.WorkEntries.Update(existingWorkEntry);
            await context.SaveChangesAsync();
        }
    }
}
