using System.Collections;
using Microsoft.AspNetCore.Mvc;
using StateManagement.Application.Flow;
using StateManagement.Contract.Flow.Requests;

namespace StateManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowController : ControllerBase
    {
        private readonly IFlowService _flowService;

        public FlowController(IFlowService flowService)
        {
            _flowService = flowService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _flowService.GetFlowAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostFlowRequest request)
        {
            var result = await _flowService.CreateFlowAsync(request);

            return Created($"flow/{result.Id}", result);
        }

        [HttpPatch("{id:long}")]
        public async Task<IActionResult> Patch([FromRoute] long id, [FromBody] PatchFlowRequest request)
        {
            if (request.Id != id) return BadRequest();

            var result = await _flowService.UpdateFlowAsync(request);

            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _flowService.DeleteFlowAsync(id);

            return NoContent();
        }
    }
}