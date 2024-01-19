using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : NetworkBehaviour
{
    public UnityEvent onComplete;
    public bool GameCompleted { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

            if (player.IsLocalPlayer)
            {
                WinGameServerRpc();
            }
        }
    }

    [ServerRpc]
    public void WinGameServerRpc()
    {
        WinGameClientRpc();
    }

    [ClientRpc]
    public void WinGameClientRpc()
    {
        GameCompleted = true;
        onComplete?.Invoke();
    }
}