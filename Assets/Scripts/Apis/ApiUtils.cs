using Newtonsoft.Json;
using UnityEngine;

public class ApiUtils
{
    public enum AuthTokenType
    {
        AccessToken,
        RefreshToken
    }

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

}