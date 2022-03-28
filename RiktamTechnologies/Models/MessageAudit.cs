using System;
using System.Collections.Generic;

namespace RiktamTechnologies.Models
{
    public partial class MessageAudit
    {
        public int AuditId { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public string? MessageText { get; set; }
        public DateTime? AuditDate { get; set; }
        public int? LikeCount { get; set; }
        public int? DisLikeCount { get; set; }
    }
}
