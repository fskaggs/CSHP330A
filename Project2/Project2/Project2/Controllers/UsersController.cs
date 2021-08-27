using Microsoft.AspNetCore.Mvc;
using Project2.Models;
using System.Collections.Generic;
using UserRepository;
using UserRepository.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepository<UserModelRepo> usersRepository;

        public UsersController(IUserRepository<UserModelRepo> Repo)
        {
            usersRepository = Repo;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserModelRepo> Get()
        {
            var users = usersRepository.GetAll();

            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = usersRepository.Get(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModelRepo value)
        {
            if (ModelState.IsValid == true)
            {
                var user = usersRepository.Add(value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
                //return Created("~api/users", user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UserModelRepo value)
        {
            if (ModelState.IsValid == true)
            {
                if (usersRepository.Get(id) == null)
                    return NotFound(new ErrorResponse() { Message = $"User record with id '{id}' not found", Data = value });

                var userRec = usersRepository.Update(value);
                if (userRec == null)
                    return BadRequest(new ErrorResponse() { Message = "Failed to update user information.", Data = value });

                return Ok(userRec);
            }

            return BadRequest(ModelState);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (ModelState.IsValid == true)
            {
                if (usersRepository.Delete(id) == true)
                    return Ok();

                return NotFound(new ErrorResponse() { Message = $"User Id '{id}' not found", Data = id });
            }
                
            return BadRequest(ModelState);
        }
    }
}
