using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using UnityEngine;

using static Constants.ButtonNames;

public class RelayUtils
{
    public static async Task<string> CreateRelay()
    {
        try
        {
            var allocation = await RelayService.Instance.CreateAllocationAsync(2);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );

            return joinCode;

        } catch (RelayServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return null;
        }
    }

    public static async Task<bool> JoinRelay(string joinCode)
    {
        try
        {
            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
                );

            return true;
        }
        catch (RelayServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return true;
        }
    }

    private static void HandleTimedOut(RelayServiceException ex)
    {
        if (ex.ErrorCode == 15998)
        {
            AlertController.Instance.Show(
          AlertCaption.Error,
          "The request to relay service has timed out. Please try again later.",
          new List<AlertButton>()
          {
                  new AlertButton()
                  {
                      ButtonText = CANCEL,
                      Script = typeof(AlertCancelButtonController)
                  }
          });
        }
       
    }
}