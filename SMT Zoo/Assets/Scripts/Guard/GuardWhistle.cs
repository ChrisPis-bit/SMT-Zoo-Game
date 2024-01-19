using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhistleType
{
    Far,
    Near,
    Close
}
public class GuardWhistle : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _emitter;
    [SerializeField] private Transform _player;

    [SerializeField] private float _distFar = 25.0f;
    [SerializeField] private float _distClose = 10.0f;

    private WhistleType _currentType;

    private void Update()
    {
        WhistleType type = WhistleType.Far;

        float dist = Vector3.Distance(_player.position, transform.position);
        if (dist <= _distClose)
            type = WhistleType.Close;
        else if (dist <= _distFar)
            type = WhistleType.Near;

        if (type == _currentType)
            return;

        _currentType = type;
        _emitter.SetParameter("WhistleType", (float)_currentType);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _distFar);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distClose);
    }
}
