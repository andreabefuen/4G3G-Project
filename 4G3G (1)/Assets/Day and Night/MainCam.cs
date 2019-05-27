using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public Transform targetPosition;

    private void FixedUpdate()
    {
        transform.LookAt(targetPosition.position);
    }
}
