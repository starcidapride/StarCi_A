using Newtonsoft.Json;
using System.Net.Http;
using UnityEngine;

public enum AuthTokenType
{
    AccessToken,
    RefreshToken
}

public class ApiUtils
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
    public static string GetAuthTokenFromPlayPrefs(AuthTokenType tokenType)
    {
        return tokenType switch
        {
            AuthTokenType.AccessToken => PlayerPrefs.GetString("accessToken"),
            _ => PlayerPrefs.GetString("refreshToken"),
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
}

public delegate void ClientErrorHandler(HttpRequestException ex);

public delegate void FailedResponseHandler(string response);
