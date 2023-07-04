﻿using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationUtils
{
    public static async Task<bool> InitiateAnonymousSignIn()
    {
        try
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += OnSignedIn;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            return true;
        } 
        
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);

            return false;
        }
    }

    private static void OnSignedIn()
    {
        Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
    }
}