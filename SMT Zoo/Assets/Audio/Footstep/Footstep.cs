using UnityEngine;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    private CharacterController characterController;
    private FMOD.Studio.EventInstance footstepsEvent;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepsEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps/Footstep");
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // Check if the player is moving
            if (characterController.velocity.magnitude > 0.1f)
            {
                // Play footsteps sound when the player is moving
                FMOD.Studio.PLAYBACK_STATE playbackState;
                footstepsEvent.getPlaybackState(out playbackState);
                
                if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    footstepsEvent.start();
                }
            }
            else
            {
                // Stop the footsteps sound when the player is not moving
                footstepsEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}