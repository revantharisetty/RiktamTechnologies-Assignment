using System;
using System.Collections.Generic;

namespace RiktamTechnologies.Models
{
    public partial class GroupMember
    {
        public int AuditId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
    }
}
