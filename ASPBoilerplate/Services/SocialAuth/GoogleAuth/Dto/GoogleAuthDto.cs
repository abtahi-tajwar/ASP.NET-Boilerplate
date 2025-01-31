public class GoogleTokenResponse
{
    public string access_token { get; set; }
    public string id_token { get; set; }
    public string scope { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}

// ✅ Model to map Google's user response
public class GoogleUserInfo
{
    public string sub { get; set; }  // Google User ID
    public string name { get; set; }
    public string given_name { get; set; }
    public string family_name { get; set; }
    public string picture { get; set; }
    public string email { get; set; }
    public bool email_verified { get; set; }
    public string locale { get; set; }
}

