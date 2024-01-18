
using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using Random = System.Random;

public class PlayerSpawnManager : NetworkBehaviour
{
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
}
