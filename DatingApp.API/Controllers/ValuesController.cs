using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDb _db;
        public ValuesController(AppDb db)
        {
            this._db = db;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValuesAsync()
        {
            await _db.Connection.OpenAsync();
            var query = new ValuesQuery(_db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValuesAsync(int id)
        {
            await _db.Connection.OpenAsync();
            var query = new ValuesQuery(_db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
