using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace StockTraceSystem.Application.Feature.Users.Queries.GetList
{
    public class GetListUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public ICollection<UserOperationClaimDto> UserOperationClaims { get; set; }
        //public OperationClaim OperationClaim { get; set; }
    }

    public class UserOperationClaimDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public OperationClaim OperationClaim{ get; set; }
    }
}