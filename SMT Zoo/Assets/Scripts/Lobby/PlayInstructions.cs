using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayInstructions : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Intro/Intro");
        instance.start();
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            instance.stop(STOP_MODE.IMMEDIATE);
        }
        else
        {
            instance.start();
        }

    }
    
    void OnDestroy()
    {
        if (instance.isValid())
        {
            instance.release();
            instance.stop(STOP_MODE.IMMEDIATE);
        }
    }
    
}
