﻿namespace WalkinPortalAPI.src.Mail
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
