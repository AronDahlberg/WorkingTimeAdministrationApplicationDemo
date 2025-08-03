using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WTA_Api.DTOs;
using WTA_Api.Models;
using WTA_Api.Services;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WorkEntryController(IWorkEntryService workEntryService) : Controller
    {
        private readonly IWorkEntryService workEntryService = workEntryService;

        /// <summary>
        /// Registers a new work entry for an employee.
        /// Admin users can register work entries for any employee, while non-admin users can only register their own work entries.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterWorkEntry")]
        public async Task<IActionResult> AddWorkEntry(AddWorkEntryDto entry)
        {
            if (entry == null)
            {
                return BadRequest(new { Message = "Work entry cannot be null." });
            }
            try
            {
                var tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (tokenUserId == null)
                    return Unauthorized();

                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin)
                {
                    var employeeId = User.FindFirstValue("EmployeeId");
                    if (employeeId == null || entry.EmployeeId.ToString() != employeeId)
                    {
                        return Forbid();
                    }
                }

                await workEntryService.AddWorkEntryAsync(entry);

                return Ok(new { Message = "Work entry added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the work entry.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a work entry by its ID.
        /// Only accessible by admin users.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteWorkEntry")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWorkEntry(int entryId)
        {
            if (entryId <= 0)
            {
                return BadRequest(new { Message = "Invalid work entry ID." });
            }
            try
            {
                await workEntryService.DeleteWorkEntryAsync(entryId);

                return Ok();
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new { knfEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the work entry.", Details = ex.Message });
            }

        }

        /// <summary>
        /// Updates an existing work entry.
        /// Only accessible by admin users.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateWorkEntry")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateWorkEntry(WorkEntryDto entry)
        {
            if (entry == null || entry.WorkEntryId <= 0)
            {
                return BadRequest(new { Message = "Invalid work entry data." });
            }
            try
            {
                await workEntryService.UpdateWorkEntryAsync(entry);
                return Ok(new { Message = "Work entry updated successfully." });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new { knfEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the work entry.", Details = ex.Message });
            }

        }

        /// <summary>
        /// Retrieves all work entries for a specific employee.
        /// Admin users can retrieve work entries for any employee, while non-admin users can only retrieve their own work entries.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>
        /// Returns a list of work entries for the specified employee if successful, or an error response if the employee ID is invalid or if an error occurs.
        /// </returns>
        [HttpGet]
        [Route("GetWorkEntries")]
        public async Task<ActionResult<List<WorkEntryDto>>> GetWorkEntries(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest(new { Message = "Invalid employee ID." });
            }
            try
            {
                var tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (tokenUserId == null)
                    return Unauthorized();

                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin)
                {
                    var RetrevingEmployeeId = User.FindFirstValue("EmployeeId");
                    if (RetrevingEmployeeId == null || RetrevingEmployeeId != employeeId.ToString())
                    {
                        return Forbid();
                    }
                }

                var workEntries = await workEntryService.GetWorkEntriesByEmployeeIdAsync(employeeId);

                return Ok(workEntries);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new { knfEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving work entries.", Details = ex.Message });
            }
        }
    }
}
