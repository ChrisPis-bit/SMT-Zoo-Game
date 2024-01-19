using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public AnnouncementManager announcementManager;
    public Direction direction;
    public FMODUnity.StudioEventEmitter soundEmitter;

    private void OnMouseDown()
    {
        Debug.Log("Clicked " + direction.ToString());
        if (announcementManager.PlayAnnouncement(direction))
            if (soundEmitter != null) soundEmitter.Play();

    }
}
