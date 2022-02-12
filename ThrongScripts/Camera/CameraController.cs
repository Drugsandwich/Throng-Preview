using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float edgeSize;
    private Vector3 cameraVec;
    [SerializeField]
    private float cameraSpeed;
    private float mouseScrollInput;
    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float smoothCamera;
    private Terrain m_terrain;
    private Vector3 terrainVec;

    private void Awake()
    {
        m_terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        terrainVec = m_terrain.terrainData.size;
        cameraVec = transform.position;
    }

   
    private void Update()
    {
        CameraInput();
    }

    //camera inputs
    private void CameraInput()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel"); // get the mouse scroll movement

        // if the mouse cursor is touching the edge of the screen (width) then more the camera on its x axis
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraVec.x += cameraSpeed * Time.deltaTime;
        }

        // if the mouse cursor is touching the edge of the screen (width) then more the camera on its -x axis
        if (Input.mousePosition.x < edgeSize)
        {
            cameraVec.x -= cameraSpeed * Time.deltaTime;
        }

        // if the mouse cursor is touching the edge of the screen (height) then more the camera on its z axis
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraVec.z += cameraSpeed * Time.deltaTime;
        }

        // if the mouse cursor is touching the edge of the screen (height) then more the camera on its -z axis
        if (Input.mousePosition.y < edgeSize)
        {
            cameraVec.z -= cameraSpeed * Time.deltaTime;
        }

        //if the mouse scroll input is bigger then 1 then zoom out
        if(mouseScrollInput > 0)
        {
            cameraVec.y -= zoomSpeed * Time.deltaTime;
        }
        else if(mouseScrollInput < 0) // else zoom in 
        {
            cameraVec.y += zoomSpeed * Time.deltaTime;
        }

        //set clamps on the edge of the map depending on how big the terrain is and clamp the min/max height the camera can be
        cameraVec.y = Mathf.Clamp(cameraVec.y, 38, 80);
        cameraVec.x = Mathf.Clamp(cameraVec.x,0, terrainVec.x);
        cameraVec.z = Mathf.Clamp(cameraVec.z, -20, terrainVec.z);
        //make camera movement smoother
        transform.position = Vector3.Lerp(transform.position,cameraVec, smoothCamera * Time.deltaTime);
    }
}
