using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

using static Constants.Apis.Profile;
using static ApiUtils;
using System.Net;

public class ProfileApiService
{
    public class SetupProflieRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }
    }

    public static async Task<PresentableUser> ExecuteSetupProfle(SetupProflieRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(SETUP_PROFILE_API, content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await ExecuteRefresh();

                if (refreshResponse == null)
                {
                    refreshTokenExpirationHandler?.Invoke();

                    return null;
                }

                return await ExecuteSetupProfle(request, clientErrorHandler);
            }
            if (!response.IsSuccessStatusCode)
            {
                failedResponseHandler?.Invoke(data);

                LoadingController.Instance.Hide();
                return null;
            }

            LoadingController.Instance.Hide();
            return JsonConvert.DeserializeObject<PresentableUser>(data);

        }
        catch (HttpRequestException ex)
        {
            clientErrorHandler?.Invoke(ex);

            LoadingController.Instance.Hide();
            return null;
        }
    }
}