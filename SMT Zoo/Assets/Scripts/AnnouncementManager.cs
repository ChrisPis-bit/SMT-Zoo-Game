using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class AnnouncementManager : NetworkBehaviour
{
    [SerializeField] private float _cooldown = 5.0f;
    [SerializeField] private SoundPlayer[] _soundPlayers;

    private float _currentTime = 0;

    public UnityEvent<Vector3> onSoundTriggered;

    public float CurrentCooldownTime =>_currentTime;
    public float CooldownTime => _cooldown;

    private void Update()
    {
        if (_currentTime <= 0)
            _currentTime = 0;
        else
            _currentTime -= Time.deltaTime;
    }

    public bool PlayAnnouncement(Direction dir)
    {
        if (_currentTime > 0)
            return false;

        _currentTime = _cooldown;
        PlayAnnouncementServerRpc(dir);

        return true;
    }

    [ServerRpc]
    public void PlayAnnouncementServerRpc(Direction dir)
    {
        PlayAnnouncementClientRpc(dir);
    }

    [ClientRpc]
    public void PlayAnnouncementClientRpc(Direction dir)
    {
        _soundPlayers[(int)dir].PlaySound();
        onSoundTriggered?.Invoke(_soundPlayers[(int)dir].guardTarget.position);
    }

}
