using System;
using System.Threading.Tasks;
using Framework.Services.Contracts;
using Framework.Services.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.API
{
    public class ApiControllerBase<TEntityDTO, TEntityInsertDTO, TEntityUpdateDTO, TService> : ApiControllerBaseReadOnly<TEntityDTO, TService>
        where TService : IService<TEntityDTO, TEntityInsertDTO, TEntityUpdateDTO>
    {
        public ApiControllerBase(TService service) : base(service)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<TEntityDTO>> Post([FromBody]TEntityInsertDTO entityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var inserted = await _service.Insert(entityModel);

            return Created("", inserted);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<TEntityDTO>> Put(int id, [FromBody] TEntityUpdateDTO entityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var toReturn = default(TEntityDTO);

            var result = await _service.ExistForId(id);
            if (!result)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    toReturn = await _service.Update(id, entityModel);
                }
                catch (Exception e)
                {
                    if (e.Message.Equals(Constants.DB_UPDATE_INCONGRUENCE))
                        return BadRequest("Parameter incongruence: Id provided is different from Id within the type content, cannot update DB ID field.");
                }
            }

            return Ok(toReturn);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var result = await _service.ExistForId(id);

            if (!result)
                return NotFound();
            else
                await _service.Delete(id);

            return Ok();
        }

    }
}
