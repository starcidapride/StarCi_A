using Newtonsoft.Json;

public class AuthApiDto
{
    public class AuthTokenSet
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
    public class PresentableUser
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }

    public class SignInRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class SignInResponse
    {
        [JsonProperty("authTokenSet")]
        public AuthTokenSet AuthTokenSet { get; set; }

        [JsonProperty("presentableUser")]
        public PresentableUser PresentableUser { get; set; }
    }

    public class SignUpRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("confirm")]
        public string Confirm { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}