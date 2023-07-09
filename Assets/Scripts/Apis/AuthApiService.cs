using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

using static Constants.Apis;
using static ApiUtils;
using System.Text;
using static AuthApiService;

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

    public static async Task<SignInResponse> ExecuteSignIn(SignInRequest request, FailedResponseHandler failedResponseHandler = null, ClientErrorHandler clientErrorHandler = null)
    {
        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = null;
            try
            {
                var jsonBody = JsonConvert.SerializeObject(request);

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync(SIGN_IN_API, content);

                var data = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    failedResponseHandler?.Invoke(data);
                    
                    return null;
                }

                
                return JsonConvert.DeserializeObject<SignInResponse>(data);
                
            } catch (HttpRequestException ex)
            {
                clientErrorHandler?.Invoke(ex);

                return null;
            }
        }
    }

    public static async Task<PresentableUser> ExecuteSignUp(SignUpRequest request, FailedResponseHandler failedResponseHandler = null, ClientErrorHandler clientErrorHandler = null)
    {
        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = null;
            try
            {
                var jsonBody = JsonConvert.SerializeObject(request);

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync(SIGN_UP_API, content);

                var data = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    failedResponseHandler?.Invoke(data);

                    return null;
                }


                return JsonConvert.DeserializeObject<PresentableUser>(data);

            }
            catch (HttpRequestException ex)
            {
                clientErrorHandler?.Invoke(ex);

                return null;
            }
        }
    }
}