using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouch : MonoBehaviour
{
    public GameObject camera_GameObject;

    Vector2 StartPosition;
    Vector2 DragStartPosition;
    Vector2 DragNewPosition;
    Vector2 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming;

    // Update is called once per frame
    void Update()
    {
        //Poner lo de detectar la camara si es con dedos o con el ratón
        if (Input.touchCount == 0 && isZooming)
        {
            isZooming = false;
        }

        if (Input.touchCount == 1)
        {
            if (!isZooming)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 NewPosition = GetWorldPosition();
                    Vector2 PositionDifference = NewPosition - StartPosition;
                    camera_GameObject.transform.Translate(-PositionDifference);
                }
                StartPosition = GetWorldPosition();
            }
        }
        else if (Input.touchCount == 2)
        {
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
        }
        else
        {
            return;
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
