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
    [Route("api/action")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly MongoDatabaseService _dbService;
        private readonly UserManager<User> _userManager;

        public ActionController(MongoDatabaseService dbService, UserManager<User> userManager)
        {
            _dbService = dbService;
            _userManager = userManager;
        }

        // POST api/action
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(ActionModel model)
        {
            try
            {
                var filter = Builders<Item>.Filter.Eq(item => item.Id, new ObjectId(model.ItemId));
                var item = _dbService.Items.Find(filter).First() as IActionnable;

                if (!item.Actions.Contains(model.Action))
                {
                    return BadRequest();
                }

                //TODO replace with DoActionWithPrivileges
                

                return item.DoAction(model.Action, model.Payload, new User()) ? Ok() : BadRequest();
            }
            catch( Exception e)
            {
                return BadRequest();
            }
        }
    }
}
