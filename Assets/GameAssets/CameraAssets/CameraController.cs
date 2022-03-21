using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{

    private float scrollSpeed = 4f;
    private float minY = 17f;
    private float maxY = 40f;

    private float targetCameraHeight;
    private float currentCameraHeight;
    private float minMaxHeightDiff;

    private float panSpeed = 12f;
    private float panBorderThickness = 15f;
    private float minX = 6f;
    private float maxX = 50f;
    private float minZ = -2f;
    private float maxZ = 40f;

    void Start()
    {
        currentCameraHeight = transform.position.y;
        targetCameraHeight = transform.position.y;
        minMaxHeightDiff = maxY - minY;
    }

    // Update is called once per frame
    void Update()
    {


        //camera key input sensing
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetCameraHeight -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        targetCameraHeight = Mathf.Clamp(targetCameraHeight, minY, maxY);

        if(pos.y > targetCameraHeight)
        {
            pos.y = Math.Max(pos.y + (targetCameraHeight - pos.y) / 20 - Time.deltaTime * 2, targetCameraHeight);
        } else if (pos.y < targetCameraHeight)
        {
            pos.y = Math.Min(pos.y + (targetCameraHeight - pos.y) / 20 + Time.deltaTime * 2, targetCameraHeight);
        }

        transform.position = pos;
        transform.rotation = Quaternion.Euler(getCameraAngleFromHeight(transform.position.y), 0, 0);

    }

    private float getCameraAngleFromHeight(float height)
    {
        float percentage = (height - minY) / minMaxHeightDiff;
        return 30f * percentage + 40;
    }
}
