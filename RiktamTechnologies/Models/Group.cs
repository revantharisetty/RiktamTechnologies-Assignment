using System;
using System.Collections.Generic;

namespace RiktamTechnologies.Models
{
    public partial class Group
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
