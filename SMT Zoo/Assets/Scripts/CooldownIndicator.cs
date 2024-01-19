using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownIndicator : MonoBehaviour
{
    public AnnouncementManager announcementManager;
    public Material cooldownMat;

    private void Update()
    {
        cooldownMat.SetFloat("_Cooldown", 1 - (announcementManager.CurrentCooldownTime / announcementManager.CooldownTime));
    }

}
