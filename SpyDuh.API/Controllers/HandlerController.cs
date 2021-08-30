using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.API.Models;
using SpyDuh.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SpyDuh.API.Controllers
{
    [Route("api/handler")]
    [ApiController]
    public class HandlerController : ControllerBase
    {
        HandlerRepo _repo;
        SpyRepo _spies;

        public HandlerController()
        {
            _repo = new HandlerRepo();
            _spies = new SpyRepo();
        }

        [HttpGet]
        public IActionResult GetAllHandlers()
        {
            return Ok(_repo.GetAll());
        }


        [HttpPost("new-handler")]
        public IActionResult AddHandler(Handler newHandler)
        {
            if (string.IsNullOrEmpty(newHandler.Name))
            {
                return BadRequest("Handler name required");
            }

            _repo.Add(newHandler);

            return Created("/api/handlers/1", newHandler);
        }

        [HttpGet("{handlerGuid}/ListAgencySpies")]
        public IActionResult GetSpiesByAgencyHandler(Guid handlerGuid)
        {
            StringBuilder message = new StringBuilder();
            if(_spies.GetByHandler(handlerGuid, message))
            {
                return Ok(message.ToString());
            }
            else return NotFound(message.ToString());
        }
    }
}
