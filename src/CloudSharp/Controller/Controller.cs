using System;
using System.Net;
using System.Threading.Tasks;
using CloudSharp.Service;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class Controller<ID, Model> : Controller where Model : class
    {
        private IService<ID, Model> _service;

        public Controller(IService<ID, Model> service)
        {
            this._service = service;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _service.GetList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ID id)
        {
            Model model = await _service.Get(id);
            if (model == null)
            {
                return NotFound( $"Could not find the specified resource." );
            }
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Model value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode( 422, ModelState );
            }

            try
            {
                var model = await _service.Create(value);
                return Ok(model);
            }
            catch (Exception e)
            {
                return StatusCode( (int)HttpStatusCode.InternalServerError, e);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ID id, [FromBody]Model value)
        {
            if( !ModelState.IsValid )
            {
                return StatusCode( 422, ModelState );
            }

            try
            {
                var model = await _service.Get( id );
                if (model == null)
                {
                    return NotFound();
                }

                return Ok( await _service.Update(id, value) );
            }
            catch( Exception e )
            {
                return StatusCode( (int)HttpStatusCode.InternalServerError, e);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(ID id)
        {
            return await _service.Delete(id);
        }
    }
}