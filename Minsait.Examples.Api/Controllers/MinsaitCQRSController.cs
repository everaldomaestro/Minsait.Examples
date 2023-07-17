using Microsoft.AspNetCore.Mvc;
using Minsait.Examples.Application.Commands;
using Minsait.Examples.Application.Commands.Base;
using Minsait.Examples.Application.Responses;
using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;

namespace Minsait.Examples.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinsaitCQRSController : ControllerBase
    {
        private readonly ILogger<MinsaitCQRSController> _logger;
        private readonly ICommandHandler<CreateMinsaitTestCommand, CreateMinsaitTestResult> _createMinsaitTestCommandHandler;
        private readonly IMinsaitTestQueries _minsaitTestQueries;

        public MinsaitCQRSController(
            ILogger<MinsaitCQRSController> logger,
            ICommandHandler<CreateMinsaitTestCommand, CreateMinsaitTestResult> createMinsaitTestCommandHandler,
            IMinsaitTestQueries minsaitTestQueries)
        {
            _logger = logger;
            _createMinsaitTestCommandHandler = createMinsaitTestCommandHandler;
            _minsaitTestQueries = minsaitTestQueries;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MinsaitTest>> GetByIdAsync(long id)
        {
            try
            {
                var result = await _minsaitTestQueries.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            try
            {
                var result = _minsaitTestQueries.GetQueryable().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post(CreateMinsaitTestCommand command)
        {
            try
            {
                var result = await _createMinsaitTestCommandHandler.Handle(command);

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
