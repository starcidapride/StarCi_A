using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

using static Constants.Apis.Profile;
using static ApiUtils;

public class ProfileApiService
{
    public class InitUserDetailsRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }
    }

    public static async Task<PresentableUser> ExecuteInitUserDetails(InitUserDetailsRequest request, ClientErrorHandler clientErrorHandler = null, FailedResponseHandler failedResponseHandler = null)
    {
        using var client = new HttpClient();

        try
        {
            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(SETUP_PROFILE_API, content);

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