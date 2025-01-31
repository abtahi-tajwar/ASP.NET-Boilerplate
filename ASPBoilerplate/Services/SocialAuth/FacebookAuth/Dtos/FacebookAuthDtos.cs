using System.ComponentModel.DataAnnotations;

namespace ASPBoilerplate.Services;

public class FacebookAuthSigninRequestPayload {
    [Required]
    public string AccessToken { get; set; }
}