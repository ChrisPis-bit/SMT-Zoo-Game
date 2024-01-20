using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Footstepsoundeffects : MonoBehaviour
{
    public EventReference footstepSound;
    public float stepDistance = 1.5f; // I don't which distance will make the footsteps sound natural, might have to be altered
    private float accumulatedDistance = 0f;
    private Vector3 posLastFrame;

    private void Start()
    {
        posLastFrame = transform.position;
    }

    private void Update()   //So the code checks how much distance the player is covering, if the player walks against a wall, there should be not footsteps
    {
        float playerMovement = Vector3.Distance(posLastFrame, transform.position);

        accumulatedDistance += playerMovement;

        if (accumulatedDistance >= stepDistance)
        {
            PlayFootstepSound();
            accumulatedDistance = 0f;
        }

        posLastFrame = transform.position;
    }

    private void PlayFootstepSound()
    {
        EventInstance instance = RuntimeManager.CreateInstance(footstepSound);
        instance.start();
        instance.release();
    }
}