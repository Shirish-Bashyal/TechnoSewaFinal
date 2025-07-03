using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;
using Domain.Entities.User;

namespace Domain.Entities
{
    public class Notification : DateAuditedEntity<int>
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
