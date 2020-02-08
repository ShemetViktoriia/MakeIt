using MakeIt.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Threading.Tasks;

namespace MakeIt.BLL.IdentityConfig
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(UserStore store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            this.EmailService = new EmailService();

            IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
            this.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtector)
            {
                TokenLifespan = TimeSpan.FromMinutes(30)
            };
        }

        public override Task<IdentityResult> CreateAsync(User user)
        {
            user.CreateDateTime = user.EditDateTime = DateTime.UtcNow;
            return base.CreateAsync(user);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.CreateDateTime = user.EditDateTime = DateTime.UtcNow;
            return base.CreateAsync(user, password);
        }

        public override Task<IdentityResult> UpdateAsync(User user)
        {
            user.EditDateTime = DateTime.UtcNow;
            return base.UpdateAsync(user);
        }

        public async Task<User> GetUserNameAsync(User user)
        {
            return await Store.FindByNameAsync(user.UserName);
        }
    }
}
