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

        public HandlerController(HandlerRepo handlerRepo, SpyRepo spyRepo)
        {
            _repo = handlerRepo;
            _spies = spyRepo;
        }

        [HttpGet]
        public IActionResult GetAllHandlers()
        {
            var spyList = _repo.GetAll();
            if (spyList.Count() == 0)
            {
                return NotFound("No handlers in database.");
            }
            else return Ok(spyList);
        }

        [HttpGet("{handlerGuid}")]
        public IActionResult GetHandler(Guid handlerGuid)
        {
            var handler = _repo.GetHandlerById(handlerGuid);
            if (handler == null)
            {
                return NotFound($"Handler with Id: {handlerGuid} not found");
            }
            else return Ok(handler);
        }


        [HttpPost("new-handler")]
        public IActionResult AddHandler(Handler newHandler)
        {
            if (string.IsNullOrEmpty(newHandler.Name))
            {
                return BadRequest("Handler name required");
            }

            if (_repo.Add(newHandler))
            {
                return Created($"/api/handler/{newHandler.Id}", newHandler);
            }
            else return BadRequest($"Unable to add Handler with name {newHandler.Name}");

        }

        [HttpPost("add-spy")]

        public IActionResult AddHandlerSpy(Guid handlerGuid, Guid spyGuid)
        {
            var handler = _repo.GetHandlerById(handlerGuid);
            var spy = _spies.GetSpy(spyGuid);
            if (handler != null && spy != null)
            {
                if (_repo.AddSpyToHandler(handler, spy))
                {
                    return Ok($"Spy {spy.Name} added to handler {handler.Name}");
                }
                else return BadRequest($"Unable to add Spy {spy.Name} to handler {handler.Name}.");
            }
            else return NotFound("Spy or handler id not found in database");
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
