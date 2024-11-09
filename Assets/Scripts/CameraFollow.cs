using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followTarget;

    void FixedUpdate()
    {
        transform.position = new Vector3(
                followTarget.transform.position.x, 
                followTarget.transform.position.y, 
                transform.position.z
            );
    }
}
