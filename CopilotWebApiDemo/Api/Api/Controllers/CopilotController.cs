using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("copilot")]
    public sealed class CopilotController : ControllerBase
    {
        private readonly ICopilotService _copilot;

        public CopilotController(ICopilotService copilot) => _copilot = copilot;

        [HttpPost("complete")]
        public async Task<ActionResult<CopilotResponse>> Complete([FromBody] CopilotRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req?.Prompt))
                return BadRequest("Prompt is required.");

            var text = await _copilot.CompleteAsync(req.Prompt, ct).ConfigureAwait(false);
            return Ok(new CopilotResponse { Content = text });
        }
    }
}