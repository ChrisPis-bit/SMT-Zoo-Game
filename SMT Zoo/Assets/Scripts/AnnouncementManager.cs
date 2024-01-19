using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class AnnouncementManager : NetworkBehaviour
{
    [SerializeField] private SoundPlayer[] _soundPlayers;

    [ServerRpc]
    public void PlayAnnouncementServerRpc(Direction dir)
    {
        PlayAnnouncementClientRpc(dir);
    }

    [ClientRpc]
    public void PlayAnnouncementClientRpc(Direction dir)
    {
        _soundPlayers[(int)dir].PlaySound();
    }

}
