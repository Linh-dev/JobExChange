﻿namespace AuthService.DTOs
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}