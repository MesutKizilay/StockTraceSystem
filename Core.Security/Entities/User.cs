using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}