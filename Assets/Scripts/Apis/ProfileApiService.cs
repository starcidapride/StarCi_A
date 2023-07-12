﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net;

using static Constants.Apis.Profile;
using static ApiUtils;
using static ProfileApiDto;
using static AuthApiDto;


public class ProfileApiService
{
   
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
                failedResponseHandler?.Invoke(data, response.StatusCode);

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