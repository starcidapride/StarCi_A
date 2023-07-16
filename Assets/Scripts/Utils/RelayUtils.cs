using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using UnityEngine;

public class RelayResponse
{ 
    public Guid AllocationId { get; set; }

    public string JoinCode { get; set; }

}
public class RelayUtils
{
    public static async Task<RelayResponse> CreateRelay()
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

            NetworkManager.Singleton.StartHost();

            LoadingSceneManager.Instance.LoadScene(SceneName.WaitingRoom, true);

            return new RelayResponse()
            {
                AllocationId = allocation.AllocationId,
                JoinCode = joinCode
            };
        } catch (RelayServiceException ex)
        {
            Debug.Log(ex);
            return null;
        }

    }

    public static async Task<RelayResponse> JoinRelay(string joinCode)
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

            NetworkManager.Singleton.StartClient();

            return new RelayResponse()
            {
                AllocationId = joinAllocation.AllocationId,
                JoinCode = joinCode
            };
        }
        catch (RelayServiceException ex)
        {
            Debug.Log(ex);
            return null;
        }
    } 

}