using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player; 
    // Start is called before the first frame update
    void Start()
    {
        player.ScreenExited += MoveCamera;
    }

    private void MoveCamera(Vector2Int nextScreen) {
        // Debug.Log("Camera event... " + nextScreen);
        float newX = 19f * nextScreen.x;
        float newY = 10.7f * nextScreen.y;
        Camera.main.transform.position = new Vector3(newX, newY, Camera.main.transform.position.z);
    }
}
