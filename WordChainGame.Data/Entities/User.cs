

namespace WordChainGame.Data.Entities
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public User()
        {
            Words = new HashSet<Word>();
            Topics = new HashSet<Topic>();
            InappropriateWordRequests = new HashSet<InappropriateWordRequest>();
        }
        public string FullName { get; set; }
        public virtual ICollection<Word> Words { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<InappropriateWordRequest> InappropriateWordRequests { get; set; }


    }
}
