using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using ArabDevCommunity.PL.Dto;
using ArabDevCommunity.PL.Error;
using ArabDevCommunity.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace ArabDevCommunity.PL.Controllers
{
   
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMailService _mailService;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager
                                 , ITokenServices tokenServices, IMailService mailService
)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenServices = tokenServices;
            this._mailService = mailService;
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(SignUpDto model)
        {
            if (CheckedEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "Email is already in use"));
            }

            var user = new User()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new ApiResponse(400, errors));
            }

            // ✅ 📌 **إنشاء توكن لتأكيد البريد**
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"https://localhost:7226/api/account/ConfirmEmail?email={user.Email}&token={encodedToken}";

            // ✅ 📌 **إرسال البريد الإلكتروني**
            // تحتاج إلى تنفيذ `IMailService.SendEmail` لإرسال البريد الفعلي
            var email = new Email
            {
                To = user.Email,
                Subject = "Confirm Your Email",
                Body = $"Please confirm your email by clicking on the link: {confirmationLink}"
            };
            _mailService.SendEmail(email);

            return Ok(new
            {
                statuscode = 200,
                message = "Registration successful! Please check your email to confirm your account.",
                token = encodedToken  // **إضافة التوكن في الاستجابة**
            });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return BadRequest(new ApiResponse(400, "Invalid email."));

            try
            {
                // فك ترميز التوكن
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest(new ApiResponse(400, $"Error: {errors}"));
                }

                return Ok(new ApiResponse(200, "Email confirmed successfully!"));
            }
            catch (FormatException)
            {
                return BadRequest(new ApiResponse(400, "Invalid token format"));
            }
        }
        #region signup
        //Register
        // [HttpPost("SignUp")]
        //انا هرجع حاجه من نوع dto
        //public async Task<ActionResult<UserDto>> SignUp(SignUpDto model)
        //{

        //    if (CheckedEmailExists(model.Email).Result.Value)
        //    {
        //        return BadRequest(new ApiResponse(400, "email is already in use"));
        //    }

        //    var User = new User()
        //    {
        //        DisplayName = model.DisplayName,
        //        Email = model.Email,
        //        UserName=model.Email.Split('@')[0],


        //    };
        //var result=  await  _userManager.CreateAsync(User,model.Password);
        //if (!result.Succeeded)
        //{
        //    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //    return BadRequest(new ApiResponse(400, errors));
        //}




        //    // if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        //    var ReturenedUser = new UserDto()
        //    {
        //        DisplayName = User.DisplayName,
        //        Email = User.Email,
        //        Token = await _tokenServices.CreateTokenAsync(User, _userManager)
        //    };
        //    return Ok(ReturenedUser);

        //}

        #endregion




        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<ApiResponse>> ForgetPassword([FromBody] ForgetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid request data."));

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return BadRequest(new ApiResponse(400, "Email not found."));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new ApiResponse(400, "Something went wrong."));

            // 🔹 ترميز التوكن لعدم حدوث مشاكل عند الإرسال عبر الرابط
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // 🔹 تعديل الرابط ليشير إلى واجهة إعادة تعيين كلمة المرور
            var resetLink = $"https://localhost:7226/api/account/ResetPassword?email={user.Email}&token={encodedToken}";


            // 🔹 إرسال الرابط عبر البريد الإلكتروني (يجب استبدال هذا بعملية إرسال حقيقية)
            var email = new Email
            {
                To = user.Email,
                Subject = "Reset password ",
                Body = $"Please Reset  your password by clicking on the link: {resetLink}"
            };
            _mailService.SendEmail(email);
            Console.WriteLine($"Original Token: {token}");
            Console.WriteLine($"Encoded Token: {encodedToken}");

            return Ok(new
            {
                statuscode = 200,
                message = "Reset password link sent to email.",
                token = encodedToken  // **إضافة التوكن في الاستجابة**
            });
        }


        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ApiResponse>> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid request data."));

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return BadRequest(new ApiResponse(400, "Invalid email."));

            // 🔹 فك ترميز التوكن
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            // 🔹 تنفيذ إعادة تعيين كلمة المرور
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new ApiResponse(400, $"Error: {errors}"));
            }
            Console.WriteLine($"Decoded Token: {decodedToken}");


            return Ok(new ApiResponse(200, "Password reset successfully!"));
        }






        //Login
        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email); 
            if (user is null)
                return Unauthorized(new ApiResponse(401, "password or email incorrect"));

            // 📌 **التحقق من تأكيد البريد الإلكتروني**
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new ApiResponse(401, "Please confirm your email before logging in."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401, "password or email incorrect"));
            }

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            });
        }







        #region LOGIN_WITHOUT_CONFIRM
        //public async Task<ActionResult<UserDto>>Login(LoginDto model)
        //{
        //    var User= await _userManager.FindByEmailAsync(model.Email);
        //    if (User is null) return Unauthorized(new ApiResponse(401, "password or email incorrect"));


        //    var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
        //    if (!result.Succeeded)
        //    {
        //        return Unauthorized(new ApiResponse(401, "password or email incorrect"));
        //    }
        //    return Ok(new UserDto()
        //    {
        //        DisplayName = User.DisplayName,
        //        Email = User.Email,
        //        Token = await _tokenServices.CreateTokenAsync(User, _userManager)

        //    });
        //}

        // [Authorize]

        #endregion
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
