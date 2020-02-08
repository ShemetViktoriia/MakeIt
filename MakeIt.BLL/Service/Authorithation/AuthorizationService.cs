using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Helper;
using MakeIt.BLL.IdentityConfig;
using MakeIt.EF;
using MakeIt.Repository.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeIt.BLL.Service.Authorithation
{
    public interface IAuthorizationService : IEntityService<User>
    {
        Task<UserAuthDTO> FindByIdAsync(string userId);
        Task<UserAuthDTO> FindByEmailAsync(string email);
        Task<AsyncOutResult<SignInStatus, bool>> GetSignInStatus(UserAuthDTO userDto);
        System.Threading.Tasks.Task ResetAccessFailedCount(UserAuthDTO userDto);
        Task<AsyncOutResult<IdentityResult, int>> GetIdentityResult(UserAuthDTO userDto);
        Task<string> GenerateEmailConfirmationToken(int userId);
        Task<string> GeneratePasswordResetToken(int userId);
        Task<string> GenerateUserInviteToken(int userId);
        System.Threading.Tasks.Task SendEmailConfirmationToken(int userId, string callbackUrl);
        System.Threading.Tasks.Task SendEmailAsync(int userId, string subject, string body);
        bool IsTokenExpired(UserAuthDTO userDto, string code);
        Task<IdentityResult> ConfirmEmailAsync(UserAuthDTO userDto, string code);
        Task<bool> IsEmailConfirmedAsync(int userId);
        Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);
        bool IsProjectMember(int userId, int projectId);
        void SignOut();

        IEnumerable<string> GetEmailListContainsString(string pattern);
    }

    public class AuthorizationService : EntityService<User>, IAuthorizationService
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAuthenticationManager _authenticationManager;
        private IUnitOfWork _unitOfWork;

        public AuthorizationService(IMapper mapper, IUnitOfWork uow, ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        public async Task<UserAuthDTO> FindByIdAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(int.Parse(userId));
            return _mapper.Map<UserAuthDTO>(user);
        }

        public async Task<AsyncOutResult<SignInStatus, bool>> GetSignInStatus(UserAuthDTO userDto)
        {
            var result = SignInStatus.Failure; // start authorization result

            var user = await _userManager.FindByEmailAsync(userDto.Email);
            bool isEmailConfirmed = false;
            if (user != null)
            {
                isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user.Id);
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                result = await _signInManager.PasswordSignInAsync(user.UserName, userDto.Password, userDto.RememberMe, shouldLockout: true);
            }
            return new AsyncOutResult<SignInStatus, bool>(result, isEmailConfirmed);
        }

        public async System.Threading.Tasks.Task ResetAccessFailedCount(UserAuthDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user != null)
                await _userManager.ResetAccessFailedCountAsync(user.Id);
        }

        public async Task<AsyncOutResult<IdentityResult, int>> GetIdentityResult(UserAuthDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            int userId = user.Id;
            return new AsyncOutResult<IdentityResult, int>(result, userId);
        }

        public async Task<string> GenerateEmailConfirmationToken(int userId)
        {
            string code = string.Empty;
            code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            return code;
        }

        public async Task<string> GeneratePasswordResetToken(int userId)
        {
            string code = string.Empty;
            code = await _userManager.GeneratePasswordResetTokenAsync(userId);
            return code;
        }

        public async System.Threading.Tasks.Task SendEmailConfirmationToken(int userId, string callbackUrl)
        {
            await _userManager.SendEmailAsync(userId,
                   "Email Verification",
                   "To confirm your email address, please click on the link:\n" + callbackUrl);
        }

        public bool IsTokenExpired(UserAuthDTO userDto, string code)
        {
            return _userManager.IsTokenExpired(_mapper.Map<User>(userDto), code);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserAuthDTO userDto, string code)
        {
            return await _userManager.ConfirmEmailAsync(userDto.Id, code);
        }

        public async System.Threading.Tasks.Task<UserAuthDTO> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserAuthDTO>(user);
        }

        public async Task<bool> IsEmailConfirmedAsync(int userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId);
        }

        public async System.Threading.Tasks.Task SendEmailAsync(int userId, string subject, string body)
        {
            await _userManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(userId, token, newPassword);
        }
        public void SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IEnumerable<string> GetEmailListContainsString(string pattern)
        {
            return _unitOfWork.Users.Find(u => u.Email.Contains(pattern)).ToList().Select(u => u.Email);
        }

        public async Task<string> GenerateUserInviteToken(int userId)
        {
            string code = string.Empty;
            code = await _userManager.GenerateUserTokenAsync("Invite", userId);
            return code;
        }

        public bool IsProjectMember(int userId, int projectId)
        {
            var ownerId = _unitOfWork.Projects.Get(projectId).OwnerId;
            var memberIds = _unitOfWork.Projects.Get(projectId).Users.Select(m => m.Id);
            return (ownerId == userId || memberIds.Contains(userId)) ? true : false;
        }
    }
}
