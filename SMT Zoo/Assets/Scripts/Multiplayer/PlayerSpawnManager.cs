using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnManager : NetworkBehaviour
{
    public GameObject playerPrefab;
    public Transform clientTransform;

    void Start()
    {
        // Check if this is the server instance
        if (IsServer)
        {
            // Spawn the player on the server
            SpawnPlayer();
        }
    }
    
    private async void SpawnPlayer()
    {
        // Instantiate the player prefab
        GameObject player = Instantiate(playerPrefab, clientTransform.position, Quaternion.identity);

        // Ensure the player object has a NetworkObject component
        NetworkObject networkObject = player.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            if (!IsOwner)
            {
                player.transform.position = gameObject.transform.position;
            }
            player.transform.position = gameObject.transform.position;
            // Use Netcode's SpawnManager to spawn the player
            networkObject.Spawn(player);

            
        }
        else
        {
            Debug.LogError("Player prefab does not have a NetworkObject component.");
        }
    }
}
