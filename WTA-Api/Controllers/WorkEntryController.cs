using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.Models;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WorkEntryController : Controller
    {
        [HttpPost]
        [Route("RegisterWorkEntry")]
        public async Task<ActionResult<WorkEntry>> AddWorkEntry(WorkEntry entry)
        {
            throw new NotImplementedException();
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
