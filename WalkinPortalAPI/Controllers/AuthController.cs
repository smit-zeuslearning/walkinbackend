using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalkinPortalAPI.Models;
using WalkinPortalAPI.Exceptions;
using WalkinPortalAPI.src.Mail;
using Microsoft.AspNetCore.DataProtection;
using System.Runtime.CompilerServices;
using System.Web;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace WalkinPortalAPI.Controllers
{
    public class AuthenticatedResponse()
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class PasswordResetResponse()
    {
        public bool success { get; set; }
        public int statuscode { get; set; }
        public string[] error { get; set; }
    }

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IDataProtectionProvider dataProtectionProvider
         )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _dataProtectionProvider = dataProtectionProvider;
        }


        /*
            Name: Login (route: api/login)
            Function: Authenticate users
            Dependencies: Identity Framework
            Params: Expect Login Model in below sample form
                    {
                        "Email": "youremail@gmail.com",
                        "Password": "your_password" 
                    }
            Returns:
                -Success: return object containing JWT Token and Expirition time
                -Fail: Related status code
        */
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthenticatedResponse authenticatedResponse = await Login(user.Email);
                    return Ok(authenticatedResponse);
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest("All the fields are required");
            }
        }

        /*
            Name: Register(route: api/register)
            Function: Register new user
            Dependencies: Identity Framework
            Params: Expect User Model in below sample form
                    {
                        "UserName": "<string>",
                        "PasswordHash": "<string>",
                        "Firstname": "<string>",
                        "Lastname": "<string>",
                        "Email": "<string>",
                        "Resume": "<string|blob>",
                        "DisplayPicture": "<string|blob>",
                        "PortfolioUrl": <string>,
                        "GetJobUpdate": <boolean>,
                        "Role": string
                        "ContactNumbers": [
                            {
                                "CountryCode": <integer>,
                                "PhoneNumber": <integer>
                            }
                        ],
                        "Educations": [
                            {
                                "AggregatePercentage": <integer>,
                                "PassingYear": <integer>,
                                "Qualification": <string>,
                                "EducationStream": <string>,
                                "CollegeName": <string>,
                                "CollegeLocation": <string>
                            }
                        ],
                        "PreferredJobRoles": [
                            {
                                "InstructionalDesigner": <boolean>,
                                "SoftwareEnginner": <boolean>,
                                "SoftwareQualityEngineer": <boolean>
                            }
                        ],
                        "ProfessionalQualifications": [
                            {
                                "ApplicationType": <string>,
                                "TotalExperience": <integer>,
                                "OnOnticePeriod": <boolean>,
                                "LastWorkingDate": <date>,
                                "TerminationNoticeMonths": <integer>,
                                "ZeusTestLast12months": <boolean>,
                                "AppledRoldLast12months": <string>,
                                "ExpertisedTechnologies": [
                                    {
                                        "Javascript": <boolean>,
                                        "Angularjs": <boolean>,
                                        "Reactjs": <boolean>,
                                        "Nodejs": <boolean>,
                                        "Other": <string>
                                    }
                                ],
                                "FamalierTechnologies": [
                                    {
                                        "Javascript": <boolean>,
                                        "Angularjs": <boolean>,
                                        "Reactjs": <boolean>,
                                        "Nodejs": <boolean>,
                                        "Other": <string>
                                    }]
                            }]
                       }
            Returns:
                -Success: return object containing JWT Token and Expirition time
                -Fail: Related status code
        */
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> RegisterUser(User newUser)
        {
            try
            {
                if (newUser.Email != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(newUser.Email);
                    if (userExists != null)
                    {
                        throw new UserAlreadyExistsException();
                    }

                    // Register user(adding data into database)
                    await _userManager.CreateAsync(newUser, newUser.PasswordHash);

                    // Adding role to the user
                    var createdUser = await _userManager.FindByEmailAsync(newUser.Email);
                    await _userManager.AddToRoleAsync(createdUser, "employee");

                    // Login the user
                    AuthenticatedResponse authenticatedResponse = await Login(newUser.Email);

                    return Ok(authenticatedResponse);
                }
                else
                {
                    return BadRequest("Please give appropriate data!");
                }

            }
            catch (UserAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error occur while registering user!");
            }
        }

        /*
            Name: Forget Password (route: api/forgotpassword)
            Function: Send password reset link to user via email
            Dependencies: Identity Framework
            Params: Expect ForgotPassword Model in below sample form
                    {
                        "Email": "youremail@gmail.com"
                    }
            Returns:
                -Success: Status code 200
                -Fail: Related status code
        */

        [HttpPost]
        [Route("api/forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return Ok();
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var mailRequest = new MailRequest
                {
                    subject = "Walkin Portal Password Reset",
                    ToEmail = model.Email,
                    body = $"<a href='http://localhost:4200/resetforgotpassword?token={HttpUtility.UrlEncode(token)}'>Click here</a> to reset your password. If you didn't request form then please request this mail."
                };

                _emailService.SendEmailAsync(mailRequest);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/resetforgotpassword")]
        public async Task<IActionResult> ResetForgotPassword(ResetForgotPasswordModel model)
        {
            if(ModelState.IsValid) 
            {
                var userId = getUserIdFromPasswordResetToken(model.Token) ;
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return BadRequest(new PasswordResetResponse
                    {
                        success = false,
                        statuscode = 400,
                        error =  ["User not found"]
                    }) ;
                }
                var passwordResetSuccessfully = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (!passwordResetSuccessfully.Succeeded)
                {
                    List<string> errors = new List<string>();

                    foreach(var err in passwordResetSuccessfully.Errors)
                    {
                        errors.Add(err.Description);
                    }

                    return BadRequest(new PasswordResetResponse
                    {
                        success = false,
                        statuscode = 400,
                        error = errors.ToArray()
                    });
                }

                return Ok(new PasswordResetResponse
                {
                    success = true,
                    statuscode = 200,
                    error = null,
                });
            }

            return BadRequest("Make API call with valid data!");
        }

        [HttpPost]
        [Authorize]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword(ChangePasswordModel model)
        {
            string userEmail = extractEmailFromJwtToken(HttpContext);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var changePasswordSuccessfully = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);

            if (!changePasswordSuccessfully.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        // Method to extract USER EMAIL from JWT Token
        private string extractEmailFromJwtToken(HttpContext httpContext)
        {
            // Cast to claim identity
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Get list of claims
            IEnumerable<Claim> claims = identity.Claims;

            // Gets Name(email address is stored as name) from claim 
            var userEmail = claims.Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault().Value;

            return userEmail.ToString();
        }



        // Method to get user id from password reset token
        private string getUserIdFromPasswordResetToken(string token)
        {
            var protector = _dataProtectionProvider.CreateProtector("DataProtectorTokenProvider");
            var resetTokenArray = Convert.FromBase64String(token);
            var unprotectedResetTokenArray = protector.Unprotect(resetTokenArray);

            using (var ms = new MemoryStream(unprotectedResetTokenArray))
            {
                using(var reader = new BinaryReader(ms))
                {
                    reader.ReadInt64();
                    var userId = reader.ReadString();
                    return userId;
                }

            }
        }

        // Temp api to check whether backend is working or not
        [Route("checkbackend")]
        [HttpGet]
        public JsonResult checkBackend()
        {
            return new JsonResult("This is json result");
        }

        // Method to perform login using User Email
        private async Task<AuthenticatedResponse> Login(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, userEmail),
                        new Claim(ClaimTypes.Name, userEmail),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return new AuthenticatedResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }


        // Method to generate the JWT Token using auth claims
        private JwtSecurityToken GetToken(List<Claim> authclaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
