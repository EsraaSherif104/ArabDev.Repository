using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using ArabDevCommunity.PL.Dto;
using ArabDevCommunity.PL.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var User = new User()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName=model.Email.Split('@')[0],


            };
            var result=  await  _userManager.CreateAsync(User,model.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
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
            if (User is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)

            });
        }


    }
}
