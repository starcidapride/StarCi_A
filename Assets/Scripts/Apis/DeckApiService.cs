using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

using static Constants.Apis.Deck;
using static ApiUtils;


public class AddDeckRequest
{
    [JsonProperty("deckName")]
    public string DeckName;
}

public class SaveDeckRequest
{
    [JsonProperty("deckName")]
    public string DeckName;

    [JsonProperty("playCardNames")]
    public List<string> PlayCardNames;

    [JsonProperty("characterCardNames")]
    public List<string> CharacterCardNames;
}

public class DefaultDeckRequest
{
    [JsonProperty("defaultDeckIndex")]
    public int DefaultDeckIndex;
}



public class DeckApiService
{
    public static async Task<PresentableUser> ExecuteAddDeck(AddDeckRequest request, FailedResponseHandler failedResponseHandler, ClientErrorHandler clientErrorHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
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

                return await ExecuteAddDeck(request, failedResponseHandler, clientErrorHandler);
            }
            else if (!response.IsSuccessStatusCode)
            {
                failedResponseHandler?.Invoke(data, response.StatusCode);

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
        finally
        {
            LoadingController.Instance.Hide();
        }
    }

    public static async Task<PresentableUser> ExecuteSaveDeck(SaveDeckRequest request, ClientErrorHandler clientErrorHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(SAVE_DECK, content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await ExecuteRefresh();

                if (refreshResponse == null)
                {
                    refreshTokenExpirationHandler?.Invoke();

                    return null;
                }

                return await ExecuteSaveDeck(request, clientErrorHandler);
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

    public static async Task<PresentableUser> ExecuteDefaultDeck(DefaultDeckRequest request, ClientErrorHandler clientErrorHandler = null, RefreshTokenExpirationHandler refreshTokenExpirationHandler = null)
    {
        using var client = new HttpClient();

        LoadingController.Instance.Show();
        try
        {
            AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.AccessToken);

            var jsonBody = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(DEFAULT_DECK, content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await ExecuteRefresh();

                if (refreshResponse == null)
                {
                    refreshTokenExpirationHandler?.Invoke();

                    return null;
                }

                return await ExecuteDefaultDeck(request, clientErrorHandler);
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