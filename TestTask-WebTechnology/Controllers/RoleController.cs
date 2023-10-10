using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using TestTask_WebTechnology.DTO;
using TestTask_WebTechnology.Interfaces;
using TestTask_WebTechnology.Models;
using TestTask_WebTechnology.QueryParameters;

namespace TestTask_WebTechnology.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        /*[HttpGet]
        [Route("role/role-list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [SwaggerOperation(Summary = "Gets all roles returns a list of DTO's")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();
            var rolesDTO = _mapper.Map<List<Role>>(roles);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rolesDTO);
        }*/

        [HttpGet]
        [Route("role/role-list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [SwaggerOperation(Summary = "Gets all roles returns a list of DTO's")]
        public async Task<IActionResult> GetRoles([FromQuery] RoleQueryParameters parameters)
        {
            var roles = await _roleRepository.GetRoles();

            if (parameters.RoleType != null)
            {
                roles = roles.Where(r => r.RoleType == parameters.RoleType).ToList();
            }

            switch (parameters.OrderBy)
            {
                case "RoleType":
                    roles = parameters.SortOrder == "desc" ? roles.OrderByDescending(r => r.RoleType).ToList() : roles.OrderBy(r => r.RoleType).ToList();
                    break;
                default:
                    break;
            }

            roles = roles.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToList();

            var roleDTO = _mapper.Map<List<Role>>(roles);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roleDTO);
        }

        [HttpGet("role/{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Gets role by Id returns DTO")]
        public async Task<IActionResult> GetRole(int roleId)
        {

            if (!await _roleRepository.RoleExists(roleId))
                return NotFound();

            var role = await _roleRepository.GetRole(roleId);
            var roleDTO = _mapper.Map<Role>(role);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roleDTO);
        }

        [HttpGet("user-role/{userId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Gets all roles of a user by Id returns a DTO")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var roles = await _roleRepository.GetUserRoles(userId);
            var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rolesDTO);
        }

        [HttpPost("add-role/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Adds role to a specific user with userId")]
        public async Task<IActionResult> AddUserRole(int userId, [FromBody] RoleDTO roleToAdd)
        {
            if (roleToAdd == null)
                return BadRequest(ModelState);

            var result = await _roleRepository.AddRolesToUser(userId, _mapper.Map<Role>(roleToAdd));
            
            if(!result)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok("Role added succesfully");
        }

    }
}
