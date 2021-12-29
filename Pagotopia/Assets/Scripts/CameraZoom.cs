using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    void Update()
    {
        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0 | Input.GetKey("down"))
        {
            if (Camera.main.fieldOfView <= GetComponent<NewGameManager>().cameraMinZoom)
                Camera.main.fieldOfView += GetComponent<NewGameManager>().cameraZoomSpeed * Time.deltaTime;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5f;
        }
        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0 | Input.GetKey("up"))
        {
            if (Camera.main.fieldOfView > GetComponent<NewGameManager>().cameraMaxZoom)
                Camera.main.fieldOfView -= GetComponent<NewGameManager>().cameraZoomSpeed * Time.deltaTime;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5f;
        }
    }
}
