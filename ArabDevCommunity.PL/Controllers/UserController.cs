using ArabDev.Repository.Specification.UserSpecification;
using ArabDev.Services.Services.DTOS;
using ArabDev.Services.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArabDevCommunity.PL.Controllers
{
    public class UserController : APIBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {

            _userService = userService;
        }
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<UserDetailsDto>>> GetAllUSer([FromQuery] UserSpecification spec)
        {
            var result = await _userService.GetAllUserAsync(spec);
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserDetailsDto>> GetUSerById(string? id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }



    }
}