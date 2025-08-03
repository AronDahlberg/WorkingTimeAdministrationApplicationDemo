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

        [HttpGet]
        [Route("GetWorkEntries")]
        public async Task<ActionResult<List<WorkEntry>>> GetWorkEntries(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
