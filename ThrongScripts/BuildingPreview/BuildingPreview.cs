using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField]
    private Material previewMat;
    private BuildManager buildManager;
    private Transform[] buildCheckers;
    private bool isColliding;
    private bool isGrounded;
    [SerializeField]
    private float range;
    [SerializeField]
    private int readyRayCasts;

    private void Awake()
    {
        buildCheckers = new Transform[4];
        SetUpRays();
        buildManager = GameObject.FindGameObjectWithTag("Manager").transform.Find("BuildManager").GetComponent<BuildManager>();
        GetMeshRender();
    }

   //get the preview material try getting first a mesh renderer and if it has a armature try getting the skinnedMesh renderer
    private void GetMeshRender()
    {
        if (transform.GetChild(1).TryGetComponent(out MeshRenderer tempRender))
        {
            previewMat = tempRender.sharedMaterial; // the material is shared so you don't need to indevidually get all materials from all the meshes
        }
        else if(transform.GetChild(1).TryGetComponent(out SkinnedMeshRenderer tempSkinedRender))
        {
            previewMat = tempSkinedRender.sharedMaterial;// the material is shared so you don't need to indevidually get all materials from all the meshes
        }
    }

    private void Start()
    {
        buildManager.Preview_Obj = transform.parent.gameObject;
    }

    private void Update()
    {
        CanBuildChecker();
        GroundCheck();
    }

    //check if player can build if the building is grounded and is not colliding with anything(besides the terrain)
    private void CanBuildChecker()
    {
        if(isGrounded && !isColliding)
        {
            buildManager.CanBuild = true;
            SetColor(true);
        }
        else
        {
            buildManager.CanBuild = false;
            SetColor(false);
        }
    }

    ///Raycast from 4 positions 
    private void RaycastCheck()
    {
        RaycastHit hit;
        for(int i = 0; i < buildCheckers.Length; i++)
        {
            if (Physics.Raycast(buildCheckers[i].position,-buildCheckers[i].up,out hit,range))
            {
                //if the ray is touching the terrain add to readyRaycast int
                if(hit.transform.tag == "Terrain")
                {
                    readyRayCasts++;
                }
                else // else take 1 off
                {
                    readyRayCasts--;
                }
            }
            else
            {
                readyRayCasts--;
            }
        }
        MinMaxGroundChecks(); // set the min max of the int readyRaycast
    }

    //check if the structure is grounded
    private void GroundCheck()
    {
        RaycastCheck();

        if (readyRayCasts == 4) /// if all 4 rays are reaching the ground then its grounded
        {
            isGrounded = true;
        }
        else // if not its not grounded
        {
            isGrounded = false;
        }
    }

    // set the min max of the int readyRaycast
    private void MinMaxGroundChecks()
    {
        if (readyRayCasts > 4)
        {
            readyRayCasts = 4;
        }

        if (readyRayCasts < 0)
        {
            readyRayCasts = 0;
        }
    }

    //if its colliding(anything besides the terrain) then set the bool is colliding true
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Terrain")
        {
            isColliding = true;
        }
    }

    //if its staying colliding(anything besides the terrain) then set the bool is colliding true
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Terrain")
        {
            isColliding = true;
        }
    }
    //if its exiting the colliding(anything besides the terrain) then set the bool is colliding false
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Terrain")
        {
            isColliding = false;
        }
    }

    //set color of the previewMat
    private void SetColor(bool isPlacable)
    {
        //if the structure is placable then change material to green
        if (isPlacable)
        {
            previewMat.SetColor("_EmissionColor", new Color(0, 111, 0));
        }
        else//else make the material to red
        {
            previewMat.SetColor("_EmissionColor", new Color(111, 0, 0));
        }
    }

    // get all positions of the buildcheckers that are used to check if a building can be placed or not with raycasts
    private void SetUpRays()
    {
        for (int i = 0; i < buildCheckers.Length; i++)
        {
            buildCheckers[i] = transform.Find("Raycast" + i);
        }
    }
}
