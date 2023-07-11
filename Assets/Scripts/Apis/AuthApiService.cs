using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Text;


using static Constants.Apis.Authentication;
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

    public static async Task<PresentableUser> ExecuteInit(ClientErrorHandler clientErrorHandler = null)
    {
        using var client = new HttpClient();

        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var response = await client.GetAsync(INIT_API);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ExecuteRefresh();
                await ExecuteInit(clientErrorHandler);

                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<PresentableUser>(data);
            }
        }
        catch (HttpRequestException ex)
        {
            clientErrorHandler?.Invoke(ex);

            return null;
        }
    } 

    public static async Task<SignInResponse> ExecuteSignIn(SignInRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null)
    {
        using var client = new HttpClient();
        
        try
        {
            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(SIGN_IN_API, content);

            var data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                failedResponseHandler?.Invoke(data);

                return null;
            }


            return JsonConvert.DeserializeObject<SignInResponse>(data);

        }
        catch (HttpRequestException ex)
        {
            clientErrorHandler?.Invoke(ex);

            return null;
        }
    }

    public static async Task<PresentableUser> ExecuteSignUp(SignUpRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null)
    {
        using var client = new HttpClient();

        try
        {
            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(SIGN_UP_API, content);

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