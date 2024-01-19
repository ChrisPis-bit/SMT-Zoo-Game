using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // Reference to the FMODUnity.StudioEventEmitter component
    public FMODUnity.StudioEventEmitter soundEmitter;

    public void PlaySound()
    {
        if (soundEmitter != null)
        {
            // Trigger the FMOD sound event
            soundEmitter.Play();
        }
    }
}
