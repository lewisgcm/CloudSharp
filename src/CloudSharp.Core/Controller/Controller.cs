using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CloudSharp.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Service;

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
                return NotFound( new HttpError() { Message = $"Could not find the specified resource." } );
            }
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Model value)
        {
            if (!ModelState.IsValid)
            {
                var error = new HttpError() { Message = "Validation Error" };
                error["errors"] = ModelState;
                return BadRequest( error );
            }

            try
            {
                var model = _service.Create(value);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(ID id, [FromBody]Model value)
        {
            if( !ModelState.IsValid )
            {
                var error = new HttpError() { Message = "Validation Error" };
                error[ "validation" ] = ModelState;
                return BadRequest( error );
            }

            try
            {
                var model = _service.Get( id );
                if (model == null)
                {
                    return NotFound();
                }

                return Ok( _service.Update(id, value) );
            }
            catch( Exception e )
            {
                return BadRequest( e );
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(ID id)
        {
            _service.Delete(id);
        }
    }
}