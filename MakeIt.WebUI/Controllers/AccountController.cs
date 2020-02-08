using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.WebUI.ReCaptchaV3;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        public AccountController(IAuthorizationService authorizationService, IMapper mapper) : base(mapper)
        {
            _authorizationService = authorizationService;
        }

        #region Login
        public ActionResult Login()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Project");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserAuthDTO>(model);
                var result = (await _authorizationService.GetSignInStatus(userDTO)).Result(out bool isEmailConfirmed);

                if (!isEmailConfirmed)
                {
                    ModelState.AddModelError("Email", "Email isn`t confirmed");
                    return View(model);
                }

                switch (result)
                {
                    case SignInStatus.Success:
                        await _authorizationService.ResetAccessFailedCount(userDTO);

                        if (true) // TODO check role
                            return Redirect("/Project/Index");

                    //ModelState.AddModelError("Password", "Not enough access rights!");
                    //return View(model);

                    case SignInStatus.LockedOut:
                        return View("BlockedAccount");

                    case SignInStatus.Failure:
                        ModelState.AddModelError("Password", "Wrong password");
                        return View(model);

                    default:
                        ModelState.AddModelError("Password", "Login Failed");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Wrong LogIn");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Register
        public ActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel(ConfigurationManager.AppSettings["RecaptchaPublicKey"]));
        }

        [HttpPost]
        [ValidateRecaptcha]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserAuthDTO>(model);
                var result = (await _authorizationService.GetIdentityResult(userDTO)).Result(out int userId);

                if (result.Succeeded)
                {
                    // TODO add to role

                    var code = await _authorizationService.GenerateEmailConfirmationToken(userId);

                    var callbackUrl = Url.Action("ConfirmUserEmail", "Account",
                                      new { userId = userId, code = code }, protocol: Request.Url.Scheme);


                    await _authorizationService.SendEmailConfirmationToken(userId, callbackUrl);

                    return RedirectToAction("ConfirmEmail", "Account", new { email = model.Email });
                }
                ModelState.AddModelError("Email", result.Errors.ElementAt(0));
            }
            return View(new RegisterViewModel(ConfigurationManager.AppSettings["RecaptchaPublicKey"])); ;
        }
        #endregion

        #region ConfirmEmail
        public ActionResult ConfirmEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        public async Task<ActionResult> ConfirmUserEmail(string userId, string code)
        {
            var userDTO = await _authorizationService.FindByIdAsync(userId);
            if (_authorizationService.IsTokenExpired(userDTO, code))
            {
                return View("CrashedLink");
            }
            var result = await _authorizationService.ConfirmEmailAsync(userDTO, code);
            if (result.Succeeded)
                return View("DisplayConfirmedEmail");
            AddErrors(result);
            return View();
        }
        #endregion

        #region ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = await _authorizationService.FindByEmailAsync(model.Email);
                if (userDTO == null)
                {
                    ModelState.AddModelError("Email", "Incorrect Email");
                    return View(model);
                }
                if (!(await _authorizationService.IsEmailConfirmedAsync(userDTO.Id)))
                {
                    ModelState.AddModelError("Email", "Email isn`t confirmed");
                    return View(model);
                }
        
                // Sending Email
                var code = await _authorizationService.GeneratePasswordResetToken(userDTO.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                                            new { UserId = userDTO.Id, code = code }, protocol: Request.Url.Scheme);

                await _authorizationService.SendEmailAsync(userDTO.Id, "Account Password Reset",
                          "To continue the procedure for resetting your account password, follow the link:\n" + callbackUrl);

                return RedirectToAction("ConfirmForgotPassword", "Account", new { email = userDTO.Email });
            }
            return View(model);
        }

        public ActionResult CrashedLinkResetPassword()
        {
            return View();
        }

        public ActionResult ConfirmForgotPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        #endregion

        #region ResetPassword
        public async Task<ActionResult> ResetPassword(string userId, string code)
        {
            TempData["UserId"] = userId;
            var userDTO = await _authorizationService.FindByIdAsync(userId);
            if (userDTO == null)
            {
                ViewBag.errorMessage = "User is not found.";
                return View("Error");
            }
            if (_authorizationService.IsTokenExpired(userDTO, code) || code == null)
            {
                return View("CrashedLink");
            }
            return View("ResetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = TempData["UserId"].ToString();
            var userDTO = await _authorizationService.FindByIdAsync(userId);

            if (_authorizationService.IsTokenExpired(userDTO, model.Code))
            {
                return View("CrashedLink");
            }

            if (userDTO == null)
            {
                ModelState.AddModelError("Email", "Incorrect Email");
                return View(model);
            }
            var result = await _authorizationService.ResetPasswordAsync(int.Parse(userId), model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("DisplayPasswordWasChanged", "Account");
            }
            AddErrors(result);
            return View();
        }

        public ActionResult DisplayPasswordWasChanged()
        {
            return View();
        }
        #endregion

        public ActionResult LogOff()
        {
            _authorizationService.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public JsonResult GetUserEmailList(string q)
        {
            var emailListDTO = _authorizationService.GetEmailListContainsString(q);
            return Json(emailListDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendInviteEmail(string email, int projectId)
        {
            var userDTO = await _authorizationService.FindByEmailAsync(email);
            if (userDTO==null)
            {
                TempData["Message"] = "Incorrect email";
                return RedirectToAction("Edit", "Project", new { projectId });
            }
            var isUserMemberOfProject = _authorizationService.IsProjectMember(userDTO.Id, projectId);
            if (isUserMemberOfProject)
            {
                TempData["Message"] = "User is already a project member";
                return RedirectToAction("Edit", "Project", new { projectId });
            }
            var code = await _authorizationService.GenerateUserInviteToken(userDTO.Id);
            var callbackUrl = Url.Action("ConfirmInvite", "Project",
                                        new { userId = userDTO.Id,  projectId = projectId, code = code }, protocol: Request.Url.Scheme);
            await _authorizationService.SendEmailAsync(userDTO.Id, "MakeIt / Join to project",
                       "If you want to join to project, please follow the link:\n" + callbackUrl);
            TempData["Message"] = "Email was sent";
            return RedirectToAction("Edit", "Project", new { projectId });
        }

        #region Additional methods
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        } 
        #endregion
    }
}