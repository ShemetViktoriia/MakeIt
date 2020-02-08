using System.Configuration;
using System.Web.Mvc;

namespace MakeIt.WebUI.ReCaptchaV3
{
    public class ValidateRecaptchaAttribute : ActionFilterAttribute
    {
        private const string RECAPTCHA_RESPONSE_KEY = "g-recaptcha-response";

        public ICaptchaValidationService CaptchaService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValidate = new InvisibleRecaptchaValidationService(ConfigurationManager.AppSettings["RecaptchaSecretKey"]).Validate(filterContext.HttpContext.Request[RECAPTCHA_RESPONSE_KEY]);
            if (!isValidate)
                filterContext.Controller.ViewData.ModelState.AddModelError("Recaptcha", "Captcha validation failed.");
        }
    }
}