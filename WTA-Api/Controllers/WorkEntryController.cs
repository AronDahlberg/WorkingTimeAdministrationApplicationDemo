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
        public async Task<IActionResult> AddWorkEntry(WorkEntryDto entry)
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
        public async Task<ActionResult<WorkEntry>> DeleteWorkEntry(int entryId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("UpdateWorkEntry")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorkEntry>> UpdateWorkEntry(WorkEntry entry)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetWorkEntries")]
        public async Task<ActionResult<List<WorkEntry>>> GetWorkEntries(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
