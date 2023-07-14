using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Text;

using static Constants.Apis.Deck;
using static ApiUtils;


public class AddDeckRequest
{
    [JsonProperty("deckName")]
    public string DeckName;
}



public class DeckApiService
{
    public static async Task<PresentableUser> ExecuteAddDeck(AddDeckRequest request, ClientErrorHandler clientErrorHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(ADD_DECK, content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await ExecuteRefresh();

                if (refreshResponse == null)
                {
                    refreshTokenExpirationHandler?.Invoke();
                    
                    return null;
                }

                return await ExecuteAddDeck(request, clientErrorHandler);
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
}