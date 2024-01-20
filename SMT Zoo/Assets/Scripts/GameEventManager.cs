using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : NetworkBehaviour
{
    private const string CSV_NAME = "Results";

    public UnityEvent onComplete;
    public UnityEvent onFail;

    public bool GameCompleted { get; private set; }

    private float _time = 0;
    private int _failCount = 0;

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

    private void Awake()
    {
        GuardAI.onPlayerCaught += OnFailServerRpc;
    }

    private void OnDestroy()
    {
        GuardAI.onPlayerCaught -= OnFailServerRpc;
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    [ServerRpc(RequireOwnership = false)]
    private void OnFailServerRpc()
    {
        OnFailClientRpc();
    }

    [ClientRpc]
    private void OnFailClientRpc()
    {
        _failCount++;
        PlayerMovement.LocalPlayer.ResetPosition();
        onFail?.Invoke();
    }

    [ServerRpc(RequireOwnership = false)]
    public void WinGameServerRpc()
    {
        WinGameClientRpc();
    }

    [ClientRpc]
    public void WinGameClientRpc()
    {
        GameCompleted = true;
        onComplete?.Invoke();
        WriteEventsToCSV();
    }

    /// <summary>
    /// Writes the current event data to a .csv file.
    /// </summary>
    public void WriteEventsToCSV()
    {
        string path = $"{Application.dataPath}/{CSV_NAME}.csv";

        if (File.Exists(path))
            File.Delete(path);

        StreamWriter writer = new StreamWriter(path);

        // Writes all event data to the .csv
        writer.WriteLine("Time, Fails");

        writer.WriteLine($"{_time}, {_failCount}");

        writer.Close();
    }

}