using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using ArabDevCommunity.PL.Dto;
using ArabDevCommunity.PL.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace ArabDevCommunity.PL.Controllers
{
   
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenServices _tokenServices;


        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager
                                 , ITokenServices tokenServices
)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenServices = tokenServices;
        }
        //Register
        [HttpPost("SignUp")]
        //انا هرجع حاجه من نوع dto
        public async Task<ActionResult<UserDto>> SignUp(SignUpDto model)
        {

            if (CheckedEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "email is already in use"));
            }

            var User = new User()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName=model.Email.Split('@')[0],


            };
            var result=  await  _userManager.CreateAsync(User,model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new ApiResponse(400, errors));
            }




            // if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            var ReturenedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturenedUser);

        }


        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto model)
        {
            var User= await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401, "password or email incorrect"));


            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401, "password or email incorrect"));
            }
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)

            });
        }

       // [Authorize]

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var returnedobject = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            };
            return Ok(returnedobject);
        }

        [HttpGet("emailExists")]
        //baseurl/api/account//emailexisys
        public async Task<ActionResult<bool>> CheckedEmailExists(string Email)
        {
            //  var user = await _userManager.FindByEmailAsync(Email);
            //  if (user is null) return false;
            // else return true;
            return await _userManager.FindByEmailAsync(Email) is not null;
        }

        //SignUpGoogle     [HttpGet("SignUpGoogle")]



    }
}
