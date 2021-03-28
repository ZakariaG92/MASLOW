using MASLOW.Entities.Items;
using MASLOW.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using MASLOW.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MASLOW.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MongoDatabaseService _dbService;

        public ItemsController(MongoDatabaseService dbService)
        {
            _dbService = dbService;
        }

        // GET: api/items
        [Authorize]
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            var result = _dbService.Items.AsQueryable().ToList();
            return result; 
        }

        // GET: api/items/types
        [Authorize]
        [HttpGet("types")]
        public IEnumerable<string> Types()
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            foreach(var assemblyName in assembly.GetReferencedAssemblies())
            {
                Assembly.Load(assemblyName);
            }

            var result = AppDomain.CurrentDomain.GetAssemblies()
                .Distinct()
                .Aggregate(new List<String>(), (stack, assembly) => {
                
                    var types = assembly.GetTypes()
                     .Where(t => t.IsSubclassOf(typeof(Item)) && !t.IsAbstract && t.IsClass)
                     .Select(type => type.AssemblyQualifiedName);

                    stack.AddRange(types);
                    return stack;
            }).Distinct();

            return result;
        }

        // GET api/items/5
        [Authorize]
        [HttpGet("{id}")]
        public Item Get(string id)
        {
            var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(id));
            var result = _dbService.Items.Find(filter).First();

            return result;
        }

        // POST api/items
        [Authorize]
        [HttpPost]
        public ActionResult Post(ItemModel value)
        {
            var item = new TheKeys.TheKeysItem()
            {
                Name = value.Name,
                Payload = value.Payload
            };

            _dbService.Items.InsertOne(item);

            return Ok();
        }

        // DELETE api/items/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var filter = Builders<Item>.Filter.Eq(item => item.Id.ToString(), id);

            _dbService.Items.FindOneAndDelete(filter);

            return Ok();
        }
    }
}
