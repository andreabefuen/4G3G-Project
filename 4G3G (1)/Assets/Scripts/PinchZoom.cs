using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;


    bool cameraMovementActivate = true;

    Vector2 startPos;
    Vector2 direction;

    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {

        
       // // If there are two touches on the device...
       // if (Input.touchCount == 2)
       // {
       //     TouchZoom();
       // }
       // MouseZoom();


        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    Vector3 movement = new Vector3(direction.normalized.x * panSpeed , 7.55f, direction.normalized.y * panSpeed);
                   
                    camera.transform.Translate(movement * Time.deltaTime, Space.World);
                    
                    break;
                case TouchPhase.Ended:
                    break;
            }
         
        }
        else if (Input.touchCount == 2)
        {
            TouchZoom();
        }
        MouseZoom();
    }

    void TouchZoom()
    {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Find the difference in the distances between each frame.
        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


        Vector3 pos = camera.transform.position; //current position

        pos.y += deltaMagnitudeDiff  * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        camera.transform.position = pos;
      //
      // // If the camera is orthographic...
      // if (camera.orthographic)
      // {
      //     // ... change the orthographic size based on the change in distance between the touches.
      //     camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
      //
      //     // Make sure the orthographic size never drops below zero.
      //     camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
      // }
      // else
      // {
      //     // Otherwise change the field of view based on the change in distance between the touches.
      //     camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
      //
      //     // Clamp the field of view to make sure it's between 0 and 180.
      //     camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
      // }
    }
    
    void MouseZoom()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            //No queremos el local, queremos el global de la camara
            camera.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            camera.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            camera.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            camera.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = camera.transform.position; //current position

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        camera.transform.position = pos;
    }
}
