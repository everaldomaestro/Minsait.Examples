using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minsait.Examples.Api.ViewModels;
using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Application.Services;

namespace Minsait.Examples.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinsaitController : ControllerBase
    {
        private readonly IMinsaitTestService _testService;
        private readonly ILogger<MinsaitController> _logger;
        private readonly IMapper _mapper;

        public MinsaitController(
            ILogger<MinsaitController> logger,
            IMinsaitTestService testService,
            IMapper mapper)
        {
            _logger = logger;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MinsaitTestViewModel>> Get(long id)
        {
            var result = await _testService.GetByIdAsync(id);
            var viewModel = _mapper.Map<MinsaitTestViewModel>(result);

            return Ok(viewModel);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<MinsaitTestViewModel>> GetAll()
        {
            var result = _testService.GetQueryable().ToList();
            var viewModels = _mapper.Map<IEnumerable<MinsaitTestViewModel>>(result);

            return Ok(viewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MinsaitTestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }

            var model = _mapper.Map<MinsaitTest>(viewModel);
            await _testService.AddAsync(model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(MinsaitTestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<MinsaitTest>(viewModel);
            await _testService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _testService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
