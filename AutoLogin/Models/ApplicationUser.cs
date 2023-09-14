using Microsoft.AspNetCore.Identity;

namespace AutoLogin.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            GuidforUser = Guid.NewGuid();
        }
        public Guid GuidforUser { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        //public string PhoneNumber { get; set; } = null!;

        //public string Email { get; set; } = null!;

        //public string? UserName { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
        public DateTime? ModificationTime { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}
