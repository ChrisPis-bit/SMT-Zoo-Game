using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;

public class WestClickDetector : MonoBehaviour
{
    // Reference to the FMODUnity.StudioEventEmitter component
    public FMODUnity.StudioEventEmitter soundEmitter;

    private void OnMouseDown()
    {
        if (soundEmitter != null)
        {
            // Trigger the FMOD sound event
            soundEmitter.Play();
        }
    }
}