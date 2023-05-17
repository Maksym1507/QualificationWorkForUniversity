﻿namespace QualificationWorkForUniversity.Models.Requests.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email not indicated")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password not indicated")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}