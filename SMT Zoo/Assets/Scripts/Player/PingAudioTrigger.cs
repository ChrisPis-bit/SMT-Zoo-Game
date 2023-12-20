using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingAudioTrigger : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _pingEvent;
    [SerializeField] private bool _overrideAttenuation = false;
    [SerializeField] private Vector2 _attenuation = new Vector2(0, 20);

    /// <summary>
    /// TODO: Trigger when VI player receives ping input.
    /// </summary>
    /// <param name="mapPos"></param>
    public void TriggerPing(Vector2 mapPos)
    {
        Vector3 position = new Vector3(mapPos.x, 0 , mapPos.y);

        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(_pingEvent);

        if (_overrideAttenuation)
        {
            instance.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, _attenuation.x);
            instance.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, _attenuation.y);
        }

        instance.set3DAttributes(position.To3DAttributes());
        instance.start();
        instance.release();
    }
}
