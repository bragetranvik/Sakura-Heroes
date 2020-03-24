using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour {
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
    public Vector3 playerChange;
    private CameraMovement cam;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ChangeRoomWithNewCameraPos(other);
    }

    /// <summary>
    /// Sets a new max/min camera position and teleport the player to a new position.
    /// </summary>
    /// <param name="other">The collider</param>
    private void ChangeRoomWithNewCameraPos(Collider2D other) {
        if (other.CompareTag("Player")) {
            cam.minPosition = cameraChangeMin;
            cam.maxPosition = cameraChangeMax;
            other.transform.position = playerChange;
        }
    }
}
