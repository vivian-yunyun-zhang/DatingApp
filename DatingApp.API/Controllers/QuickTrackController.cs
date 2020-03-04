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
    public class QuickTrackController : ControllerBase
    {
        private readonly AppDb _db;
        public QuickTrackController(AppDb db)
        {
            this._db = db;

        }
        // GET api/quicktrack
        [HttpGet]
        public async Task<IActionResult> GetQuickTrackAsync()
        {
            await _db.Connection.OpenAsync();
            var query = new QuickTrackQuery(_db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/quicktrack/5/2020-02-19

        // [Route("api/[controller]/{id}/{job_date}")]
        [HttpGet("{id}/{job_date}")]
        public async Task<IActionResult> GetQuickTrackAsync(int id,
                                                            string job_date)
        {
            Console.Write("ID: " + id + ";JOBDATE:" + job_date);
            await _db.Connection.OpenAsync();
            var query = new QuickTrackQuery(_db);
            var result = await query.FindOneAsync(id, job_date);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/quicktrack
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/quicktrack/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/quicktrack/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    internal class FromURIAttribute : Attribute
    {
    }
}
