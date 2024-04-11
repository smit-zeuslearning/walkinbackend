using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkinPortalAPI.src.Mail;

namespace WalkinPortalAPI.Controllers
{
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _mailService;

        public MailController(IEmailService mailService)
        {
            this._mailService = mailService;
        }

        [HttpGet]
        [Route("api/sendemail")]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                var mailreq = new MailRequest()
                {
                    ToEmail = "smit.dpatel9924@gmail.com",
                    subject = ".net core mail testing",
                    body = "<h1>This is testing email. Ignor if irrlavant to you</h1>",
                };

                await _mailService.SendEmailAsync(mailreq);
                return Ok("Email send successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Exception occur while sending email! " + ex.Message);
            }
        }
    }
}
