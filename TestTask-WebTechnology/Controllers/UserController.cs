using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestTask_WebTechnology.DTO;
using TestTask_WebTechnology.Interfaces;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list-users")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<User>))]
        [SwaggerOperation(Summary = "Retrieves a list of users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var userDTO = _mapper.Map<List<UserDTO>>(users);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDTO);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Retrieves a specific user by unique id")]
        public async Task<IActionResult> GetUser(int userId)
        {
            if (!await _userRepository.UserExists(userId))
                return NotFound();

            var user = await _userRepository.GetUser(userId);
            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Creates a user from a DTO, automaticaly adds a \"User\" role to it")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userCreate)
        {
            if(userCreate == null)
                return BadRequest(ModelState);

            var users = await _userRepository.GetUsers();
            var user = users.Where(u=> u.Name.Trim().ToUpper() == userCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            var userWithEmail = users.FirstOrDefault(u => u.Email == userCreate.Email);
            if (userWithEmail != null)
            {
                ModelState.AddModelError("", "Email already in use");
                return StatusCode(422, ModelState);
            }

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if(! await _userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("User succesfully created!");
        }

        
        [HttpPut("update-user/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Updates a User Object with a DTO")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDTO userUpdate)
        {
            if(userUpdate == null)
                return BadRequest(ModelState);

            if(userId != userUpdate.Id)
                return BadRequest(ModelState);

            if (!await _userRepository.UserExists(userId))
                NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(userUpdate);

            if(! await _userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("delete-user/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Deletes a user Object")]
        public async Task<IActionResult> DeleteUser(int userId) 
        {
            if(! await _userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userToDelete = await _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(! await _userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting user");
            }

            return NoContent();
        }

    }
}
