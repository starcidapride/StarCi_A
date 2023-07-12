using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

using static Constants.Apis.Authentication;
using static AuthApiDto;
using System.Net;

public enum AuthTokenType
{
    AccessToken,
    RefreshToken
}

public class ApiUtils
{

    public static string GetAuthTokenFromPlayPrefs(AuthTokenType tokenType)
    {
        return tokenType switch
        {
            AuthTokenType.AccessToken => PlayerPrefs.GetString("AccessToken"),
            _ => PlayerPrefs.GetString("RefreshToken"),
        }; 
    }

    public static void SaveAuthenticationTokens(string accessToken = null, string refreshToken = null)
    {
        if (!string.IsNullOrEmpty(accessToken))
        {
            PlayerPrefs.SetString("AccessToken", accessToken);
        }

        if (!string.IsNullOrEmpty(refreshToken))
        {
            PlayerPrefs.SetString("RefreshToken", refreshToken);
        }
    }

    public static void AttachAuthTokenToHttpRequestHeader(HttpClient client, AuthTokenType tokenType)
    {
        var token = GetAuthTokenFromPlayPrefs(tokenType);

        if (string.IsNullOrEmpty(token))
        {
            throw new HttpRequestException("Authorization token could not be retrieved from storage.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public static async Task<AuthTokenSet> ExecuteRefresh()
    {
        using var client = new HttpClient();
        AttachAuthTokenToHttpRequestHeader(client, AuthTokenType.RefreshToken);

        var response = await client.GetAsync(REFRESH_API);

        if (!response.IsSuccessStatusCode) return null;

        var data = await response.Content.ReadAsStringAsync();

        var authTokens = JsonConvert.DeserializeObject<AuthTokenSet>(data);

        SaveAuthenticationTokens(authTokens.AccessToken, authTokens.RefreshToken);

        return authTokens;
    }
}

public delegate void ClientErrorHandler(HttpRequestException ex);

public delegate void FailedResponseHandler(string response, HttpStatusCode code);

public delegate void RefreshTokenExpirationHandler();