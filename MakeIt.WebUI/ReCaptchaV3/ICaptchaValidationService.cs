namespace MakeIt.WebUI.ReCaptchaV3
{
    public interface ICaptchaValidationService
    {
        bool Validate(string response);
    }
}
