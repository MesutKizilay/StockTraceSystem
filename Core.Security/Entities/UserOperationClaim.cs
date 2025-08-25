using Core.Persistence.Repositories;
using System.Text.Json.Serialization;

namespace Core.Security.Entities
{
    public class UserOperationClaim : Entity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        //[JsonIgnore]
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}