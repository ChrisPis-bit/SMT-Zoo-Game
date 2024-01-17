using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;

public class SouthSoundPlayer : MonoBehaviour
{
   
    public FMODUnity.StudioEventEmitter soundEmitter;

    public void PlaySound()
    {
        if (soundEmitter != null)
        {
            
            soundEmitter.Play();
        }
    }
}