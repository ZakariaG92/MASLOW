using MASLOW.Entities.Items;
using MASLOW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MASLOW.Controllers
{
    [Route("api/sersors")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly MongoDatabaseService _dbService;

        public SensorsController(MongoDatabaseService dbService)
        {
            _dbService = dbService;
        }

        // GET api/sensors/5
        [HttpGet("{id}/")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(string id)
        {
            try
            {
                var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(id));
                var item = _dbService.Items.Find(filter).First();

                return Ok(item.Values);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET api/sensors/5/value
        [HttpGet("{id}/{value}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(string id, string value)
        {
            try
            {
                var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(id));
                var item = _dbService.Items.Find(filter).First();

                return Ok(item.GetValue(value));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
