﻿namespace QualificationWorkForUniversity.Models.Requests
{
    public class GetBasketRequest
    {
        [Required]
        public string UserId { get; set; } = null!;
    }
}
