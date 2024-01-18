using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestDistraction : MonoBehaviour
{
    public Transform location;

    public UnityEvent<Vector3> onTriggered;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            onTriggered?.Invoke(location.position);
    }
}
