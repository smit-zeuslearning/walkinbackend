using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WalkinPortalAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace WalkinPortalAPI.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly WalkinPortalContext _DbContext;

        public DashboardController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration
         )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _DbContext = new WalkinPortalContext();
        }

        [Authorize]
        [HttpPost]
        [Route("api/getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                var userList = await _DbContext.Users
                                        .Include(user => user.ContactNumbers)
                                        .Include(user => user.Educations)
                                        .Include(user => user.PreferredJobRoles)
                                        .Include(user => user.ProfessionalQualifications)
                                            .ThenInclude(professionalQualification => professionalQualification.ExpertisedTechnologies)
                                        .Include(user => user.ProfessionalQualifications)
                                            .ThenInclude(professionalQualification => professionalQualification.FamalierTechnologies)
                                        .ToListAsync();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected error occurs while retriving user data!" + ex.Message);
            }
        }
    }
}
