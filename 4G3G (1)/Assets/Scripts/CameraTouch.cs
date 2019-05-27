using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouch : MonoBehaviour
{
    public GameObject camera_GameObject;
    public float zoomModifierSpeed = 0.1f;
    public float movementSpeed;

    public float maxZoom = 50f;
    public float minZoom = 2f;

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
        if (!PauseMenu.GameIsPaused)
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


                /*

                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    isZooming = true;

                    DragNewPosition = GetWorldPositionOfFinger(1);
                    Vector2 PositionDifference = DragNewPosition - DragStartPosition;


                    if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                    {
                        camera_GameObject.GetComponent<Camera>().orthographicSize += Mathf.Abs(PositionDifference.magnitude * 2);

                        if (camera_GameObject.GetComponent<Camera>().orthographicSize < 0)
                        {
                            camera_GameObject.GetComponent<Camera>().orthographicSize *= -1;
                        }
                    }


                    if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                    {
                        camera_GameObject.GetComponent<Camera>().orthographicSize -= Mathf.Abs(PositionDifference.magnitude * 2);
                        if(camera_GameObject.GetComponent<Camera>().orthographicSize < 0)
                        {
                            camera_GameObject.GetComponent<Camera>().orthographicSize *= -1;
                        }
                    }


                    DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);
                }
                DragStartPosition = GetWorldPositionOfFinger(1);
                Finger0Position = GetWorldPositionOfFinger(0);
            }*/

                camera_GameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(camera_GameObject.GetComponent<Camera>().orthographicSize, 2f, maxZoom);

            }
            float auxX = Mathf.Clamp(camera_GameObject.transform.position.x, -10f, 30f);
            float auxY = Mathf.Clamp(camera_GameObject.transform.position.y, -20f, 20);

            camera_GameObject.transform.position = new Vector3(auxX, auxY, camera_GameObject.transform.position.z);
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
