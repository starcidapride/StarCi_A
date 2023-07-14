using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Text;


using static Constants.Apis.Authentication;
using static ApiUtils;

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

public class AuthApiService
{
  
    public static async Task<PresentableUser> ExecuteInit(ClientErrorHandler clientErrorHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var response = await client.GetAsync(INIT_API);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await ExecuteRefresh();

                if (refreshResponse == null)
                {
                    refreshTokenExpirationHandler?.Invoke();
                    
                    return null;
                }

                return await ExecuteInit(clientErrorHandler);
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
        finally
        {
            LoadingController.Instance.Hide();
        }
    } 

    public static async Task<SignInResponse> ExecuteSignIn(SignInRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();

        try
        {
            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(SIGN_IN_API, content);

            var data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                failedResponseHandler?.Invoke(data, response.StatusCode);

                return null;
            }

            return JsonConvert.DeserializeObject<SignInResponse>(data);

        }
        catch (HttpRequestException ex)
        {
            clientErrorHandler?.Invoke(ex);

            return null;
        }
        finally
        {
            LoadingController.Instance.Hide();
        }
    }

    public static async Task<PresentableUser> ExecuteSignUp(SignUpRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(SIGN_UP_API, content);

            var data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                failedResponseHandler?.Invoke(data, response.StatusCode);

                return null;
            }

            return JsonConvert.DeserializeObject<PresentableUser>(data);

        }
        catch (HttpRequestException ex)
        {
            clientErrorHandler?.Invoke(ex);

            return null;
        }
        finally
        {
            LoadingController.Instance.Hide();
        }
    }
}