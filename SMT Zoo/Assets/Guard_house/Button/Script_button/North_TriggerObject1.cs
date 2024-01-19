using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;

public class NorthClickDetector : MonoBehaviour
{
  
    public FMODUnity.StudioEventEmitter soundEmitter;

    private void OnMouseDown()
    {
        if (soundEmitter != null)
        {
            
            soundEmitter.Play();
        }
    }
}