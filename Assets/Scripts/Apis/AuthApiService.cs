using Newtonsoft.Json;
using System.Threading.Tasks;
using static ApiUtils;
public class AuthApiService
{
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

    public static async void ExecuteSignIn()
    {

    }
}