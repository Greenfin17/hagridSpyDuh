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
    [Route("api/Spy")]
    [ApiController]
    public class SpyController : ControllerBase
    {
        SpyRepo _repo;

        public SpyController()
        {
            _repo = new SpyRepo();
        }

        [HttpGet]
        public IActionResult GetAllSpies()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{spyGuid}")]
        public IActionResult GetSpyById(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null)
            {
                return Ok(spyObj);
            }
            returnStr.Append($"Spy with id: {spyGuid} not found\n");
            return NotFound(returnStr.ToString());
        }

        [HttpPatch("{spyGuid}/AddSkill/{spySkill}")]
        public IActionResult AddSkill(Guid spyGuid, string spySkill)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null && _repo.AddSkill(spyObj, spySkill))
            {
                return Ok($"Added skill {spySkill} to spy {spyObj.Name}");
            }
            else if (spyObj == null) returnStr.Append($"Spy with id: {spyGuid} not found\n");
            else  returnStr.Append($"Spy skill {spySkill} not found or already in list.\n");
            
            return NotFound(returnStr.ToString());

        }

        [HttpPatch("{spyGuid}/AddFriend/{friendGuid}")]
        public IActionResult AddFriend(Guid spyGuid, Guid friendGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            var friendObj = _repo.GetSpy(friendGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null && friendObj != null)
            {
                if (!spyObj.Friends.Contains(friendGuid))
                {
                    if (_repo.AddFriend(spyGuid, friendGuid)) return Ok($"Friend {friendObj.Name} with Id {friendGuid} added.\n");
                    else return NotFound($"Unable to add friend with id {friendGuid}");
                }
                else return BadRequest($"Friend {friendObj.Name} with Id: {friendGuid} is already in the friend list\n");
            }
            if (spyObj == null) returnStr.Append($"Spy with id: {spyGuid} not found\n");
            if (friendObj == null) returnStr.Append($"Friend with id: {spyGuid} not found\n");

            return NotFound(returnStr.ToString());
        }

        [HttpGet("{spyGuid}/ListFriends")]
        public IActionResult ListFriends(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            if (spyObj != null)
            {
                var response = _repo.ListFriends(spyGuid);
                if (response.Count() == 0)
                {
                    // response is valid but empty
                    return Ok("No friends (:\n");
                }
                // response is valid
                else return Ok(response);
            }
            // spy not found
            else return NotFound($"Spy with id: {spyGuid} not found");

        }

        [HttpGet("{spyGuid}/Friends/Friends")]
        public IActionResult FriendsFriends(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            if (spyObj != null)
            {
                var response = _repo.GetFriendsFriends(spyGuid);
                if (response.Count() == 0)
                {
                    // response is valid but empty
                    return Ok("No friends of friends(:\n");
                }
                else return Ok(response);
            }
            else return NotFound($"Spy with id: {spyGuid} not found");
        }


        [HttpPatch("{spyGuid}/AddEnemy/{enemyGuid}")]
        public IActionResult AddEnemy(Guid spyGuid, Guid enemyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            var enemyObj = _repo.GetSpy(enemyGuid);
            StringBuilder returnStr = new StringBuilder("");
            if (spyObj != null && enemyObj != null)
            {
                if (!spyObj.Enemies.Contains(enemyGuid))
                {
                    if (_repo.AddEnemy(spyGuid, enemyGuid)) return Ok($"Enemy {enemyObj.Name} with ID {enemyObj.Id} added.\n");
                    else return NotFound($"Unable to add enemy with Id {enemyGuid}");
                }
                else return BadRequest($"Enemy {enemyObj.Name} with ID {enemyGuid} already in enemy list\n");
            }
            if (spyObj == null) returnStr.Append($"Spy with ID {spyGuid} not found\n");
            if (enemyObj == null) returnStr.Append($"Friend with ID {spyGuid} not found\n");

            return NotFound(returnStr.ToString());
        }

        [HttpGet("{spyGuid}/ListEnemies")]
        public IActionResult ListEnemies(Guid spyGuid)
        {
            var spyObj = _repo.GetSpy(spyGuid);
            if (spyObj != null)
            {
                var response = _repo.ListEnemies(spyGuid);
                if (response.Count() == 0)
                {
                    return Ok("No enemies!\n");
                }
                else return Ok(response);
            }
            // spy not found
            else return NotFound($"Spy with ID {spyGuid} not found");

        }

        [HttpPost("new-spy")]
         public IActionResult AddSpy(Spy newSpy)
        {
            if (string.IsNullOrEmpty(newSpy.Name))
            {
                return BadRequest("Name needed");
            }

            if (_repo.AddSpy(newSpy)) return Created($"/api/spies/{newSpy.Id}", newSpy);
            else return BadRequest($"Unable to add spy with name {newSpy.Name}");
        }

        [HttpGet("skills/{skill}")]
        public IEnumerable<Spy> GetSpiesBySkill(string skill)
        {
            return _repo.GetBySkills(skill);
        }

        [HttpGet("{spyGuid}/skillsandservices")]

        public IActionResult ListSandS(Guid spyGuid)
        {
            return Ok(_repo.ListSkillsAndServices(spyGuid));
        }
    }
}
