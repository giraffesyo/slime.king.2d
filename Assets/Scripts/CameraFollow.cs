using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    // The target we are following
    public Transform target;

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        Vector3 newPos = target.position;
        newPos.z = -10f;
        transform.position = target.position;

        // Set the height of the camera
        transform.position = newPos;
    }
}