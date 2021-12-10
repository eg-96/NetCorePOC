using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Framework.Services.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Framework.API
{
    public class ApiControllerBaseReadOnly<TEntityDTO, TService> : ControllerBase 
        where TService : IServiceReadOnly<TEntityDTO>
    {
        protected readonly TService _service;

        protected ApiControllerBaseReadOnly(TService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<IEnumerable<TEntityDTO>>> Get()
            => Ok(await _service.Get());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<TEntityDTO>> GetById(int id)
        {
            var result = await _service.GetForId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<int>> GetCount()
            => Ok(await _service.GetCount());
    }
}
