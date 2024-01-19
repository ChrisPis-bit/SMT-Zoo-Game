
using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using Random = System.Random;

public class PlayerSpawnManager : NetworkBehaviour
{
    public static Vector3 StartPos;
    public GameObject playerPrefab;
    public Transform clientTransform;
    public Transform VITransform;
    
    public override void OnNetworkSpawn() {
        if (IsServer)
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, true);
        else
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, false);
    }
    public void Start()
    {
        /*if (!IsOwner)
        {
            GameObject targetObject = GameObject.FindWithTag("Player");
            targetObject.transform.position = gameObject.transform.position;
        }
        else
        {
            GameObject targetObject = GameObject.FindWithTag("Player");
            targetObject.transform.position = clientTransform.position;
        }*/
    }
    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, bool isHostPlayer) {
        GameObject newPlayer;
        StartPos = isHostPlayer ? clientTransform.position : VITransform.position;

        if (!isHostPlayer)
            newPlayer=Instantiate(playerPrefab, VITransform.position, Quaternion.identity);
        else
            newPlayer=Instantiate(playerPrefab, clientTransform	.position, Quaternion.identity);
        NetworkObject netObj=newPlayer.GetComponent<NetworkObject>();
        
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId,true);

    }
}
