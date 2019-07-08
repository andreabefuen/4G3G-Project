using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTouch : MonoBehaviour
{
    public GameObject camera_GameObject;
    public float zoomModifierSpeed = 0.1f;
    public float movementSpeed;

    public float maxZoom = 50f;
    public float minZoom = 2f;

    public float panBorderThickness = 5f;

    public bool touchScreenSupport = true;

    Vector2 StartPosition;
    Vector2 DragStartPosition;
    Vector2 DragNewPosition;
    Vector2 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming;

    float touchesPrevPosDifference, touchCurPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && UIManager.instance.cameraActivated )
        {
            //Poner lo de detectar la camara si es con dedos o con el ratón
            if (Input.touchCount == 0 && isZooming)
            {
                isZooming = false;
            }

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 NewPosition = GetWorldPosition();
                    Vector2 PositionDifference = NewPosition - StartPosition;
                    camera_GameObject.transform.Translate(-PositionDifference.x, -PositionDifference.y, movementSpeed * Time.deltaTime);


                }
                StartPosition = GetWorldPosition();
                /*
                if (!isZooming)
                {

                }*/
            }
            else if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                touchCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
                if (touchesPrevPosDifference > touchCurPosDifference)
                {
                    camera_GameObject.GetComponent<Camera>().orthographicSize += zoomModifier;
                    //isZooming = true;
                }
                if (touchesPrevPosDifference < touchCurPosDifference)
                {
                    camera_GameObject.GetComponent<Camera>().orthographicSize -= zoomModifier;
                    //isZooming = true;

                }


                camera_GameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(camera_GameObject.GetComponent<Camera>().orthographicSize, 2f, maxZoom);

            }
        }
            
        
       
    }

    Vector2 GetWorldPosition()
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetWorldPositionOfFinger(int FingerIndex)
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }
}
