using System;
using System.Collections.Generic;

namespace RiktamTechnologies.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? MobileNumber { get; set; }
        public string? UserPassword { get; set; }
    }
}
