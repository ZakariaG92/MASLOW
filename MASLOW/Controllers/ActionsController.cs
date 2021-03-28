using MASLOW.Models;
using MASLOW.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MASLOW.Entities.Items;
using MASLOW.Entities;
using Microsoft.AspNetCore.Identity;
using MASLOW.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MASLOW.Controllers
{
    [Route("api/actions")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly MongoDatabaseService _dbService;
        private readonly UserManager<User> _userManager;

        public ActionsController(MongoDatabaseService dbService, UserManager<User> userManager)
        {
            _dbService = dbService;
            _userManager = userManager;
        }

        // GET api/actions/5
        [HttpGet("{id}/")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(string id)
        {
            try
            {
                var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(id));
                var item = _dbService.Items.Find(filter).First();

                return Ok(item.Actions);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // POST api/actions
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(ActionModel model)
        {
            try
            {
                var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(model.ItemId));
                var item = _dbService.Items.Find(filter).First();

                if (!item.Actions.Contains(model.Action))
                {
                    return BadRequest();
                }

                //TODO replace with DoActionWithPrivileges and real user

                return item.DoAction(model.Action, model.Payload, new User()) ? Ok() : BadRequest();
            }
            catch( Exception e)
            {
                return BadRequest();
            }
        }
    }
}
