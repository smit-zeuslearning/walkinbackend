﻿namespace WalkinPortalAPI.Models
{
    public class ResetForgotPasswordModel
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Token { get; set; }
    }
}
