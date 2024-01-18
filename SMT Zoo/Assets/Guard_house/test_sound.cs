using UnityEngine;
using FMODUnity;

public class FMODSoundPlayer : MonoBehaviour
{
    private FMOD.Studio.EventInstance soundEvent;

    void Start()
    {
        // Create an instance of the FMOD event
        soundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Button triggers/eastern_audio");

        // Play the FMOD event immediately when the game starts
        soundEvent.start();
    }
}