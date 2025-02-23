using ArabDev.Repository.Specification.UserSpecification;
using ArabDev.Services.Services.DTOS;
using ArabDev.Services.Services.Users;
using ArabDevCommunity.PL.Error;
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

        [HttpPut("picture")]
        public async Task<ActionResult<ApiResponse>> AddOrUpdatePicture([FromForm] UserupdataPictureDTo dto)
        {
            try
            {
                var result = await _userService.AddOrUpdatePictureAsync(dto);
                return Ok(new ApiResponse(200, $"The picture has been updated successfully: {result}"));

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse(404, ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(500));
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ApiResponse>> UpdateUserDetails([FromBody] UserDetailsDto dto)
        {
            if (string.IsNullOrEmpty(dto.Id))
            {
                return BadRequest(new ApiResponse(400, "User ID is required"));
            }

            try
            {
                // 3. Search for the user
                var result = await _userService.UpdateUserDetailsAsync(dto);

                // 4. If the user is found and updated successfully, send a success response
                return Ok(new ApiResponse(200, "Data updated successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                // 5. If the user is not found
                return NotFound(new ApiResponse(404, ex.Message));
            }
            catch (Exception ex)
            {
                // 6. In case of any unexpected error
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }







    }
}