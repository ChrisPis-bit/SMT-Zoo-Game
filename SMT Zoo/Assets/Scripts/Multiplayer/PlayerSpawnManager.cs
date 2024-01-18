
using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using Random = System.Random;

public class PlayerSpawnManager : NetworkBehaviour
{
    public GameObject playerPrefab;
    public Transform clientTransform;

    public void Start()
    {
        if (!IsOwner)
        {
            GameObject targetObject = GameObject.FindWithTag("Player");
            targetObject.transform.position = gameObject.transform.position;
        }
        else
        {
            GameObject targetObject = GameObject.FindWithTag("Player");
            targetObject.transform.position = clientTransform.position;
        }
    }

    private async void SpawnPlayer()
    {
        if (IsServer)
        {
            GameObject player = Instantiate(playerPrefab, clientTransform.position, Quaternion.identity);

            // Ensure the player object has a NetworkObject component
            NetworkObject networkObject = player.GetComponent<NetworkObject>();
            if (networkObject != null)
            {

                networkObject.SpawnAsPlayerObject(GenerateRandomClientId(), player);

            }
            else
            {
                Debug.LogError("Player prefab does not have a NetworkObject component.");
            }
        }
        
    }
    private ulong GenerateRandomClientId()
    {
        Random random = new Random();

        // Generate two random uint values and combine them into a ulong
        ulong randomClientId = ((ulong)random.Next() << 32) | (uint)random.Next();

        return randomClientId;
    }
    private async void SpawnPlayerClient()
    {
        // Instantiate the player prefab
        GameObject player = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);

        // Ensure the player object has a NetworkObject component
        NetworkObject networkObject = player.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            
            networkObject.Spawn(player);

            
        }
        else
        {
            Debug.LogError("Player prefab does not have a NetworkObject component.");
        }
    }
}
