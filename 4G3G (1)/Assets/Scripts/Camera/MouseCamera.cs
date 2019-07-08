using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float panSpeed;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float maxZoom = 50f;
    public float minZoom = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1 || SystemInfo.deviceType != DeviceType.Desktop)
        {
            this.enabled = false;
        }
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                //No queremos el local, queremos el global de la camara
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }


            float scroll = Input.GetAxis("Mouse ScrollWheel");

        //Vector3 pos = transform.position; //current position
        float zoomModifier =-1 * scroll * 1000 * scrollSpeed * Time.deltaTime; 
        this.GetComponent<Camera>().orthographicSize += zoomModifier;
        this.GetComponent<Camera>().orthographicSize = Mathf.Clamp(this.GetComponent<Camera>().orthographicSize, 2f, maxZoom);

        float auxX = Mathf.Clamp(this.transform.position.x, -10f, 30f);
        float auxY = Mathf.Clamp(this.transform.position.y, -20f, 20);

        this.transform.position = new Vector3(auxX, auxY, this.transform.position.z);

        // pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //   pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // transform.position = pos;
        }
    }
}
