using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;

public class EastSoundPlayer : MonoBehaviour
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
