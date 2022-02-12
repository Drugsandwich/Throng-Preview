using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private float range = 3000f;
    private GameObject preview_Obj;
    public GameObject Preview_Obj { get { return preview_Obj; } set { preview_Obj = value; } }
    private bool canBuild;
    public bool CanBuild { get { return canBuild; } set { canBuild = value; } }
    private Transform cameraT;
    private Camera cameraComp;
    private LayerMask m_Mask;
    private Vector3 rotClampVec;
    private RaycastHit hit;
    private Resource_Loader m_Loader;
    private Resource_Holder resource_Holder;
    private int OilCost;
    private int IronCost;
    private bool isBuilding;

    [SerializeField] private int[] ironStructureCost = new int[2];
    [SerializeField] private int[] oilStructureCost = new int[2];
    [SerializeField] private int[] turretStructureCost = new int[2];
    [SerializeField] private int[] artileryStructureCost = new int[2];
    [SerializeField] private int[] sniperStructureCost = new int[2];
    private void Awake()
    {
        resource_Holder = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Holder").GetComponent<Resource_Holder>();
        m_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
        cameraT = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        cameraComp = cameraT.GetComponent<Camera>();
    }

    private void Start()
    {
        m_Mask = LayerMask.GetMask("Structure", "Enemy", "Giants","Ignore Raycast");
        m_Mask = ~m_Mask;//the given mask will ignore the set layers
    }

    private void Update()
    {
        Functions();
    }

    private void Functions()
    {
        if (preview_Obj != null)//execute the functions if 
        {
            BuildingPreviewPos();
            ConfirmBuild();
            CancelBuild();
        }
    }

    //set the  preview objects position where the mouse cursor is 
    private void BuildingPreviewPos()
    {
        if (Physics.Raycast(cameraComp.ScreenPointToRay(Input.mousePosition), out hit, range, m_Mask))
        {
            preview_Obj.transform.position = hit.point;
        }
    }

    //conforming if the building can be build or not
    private void ConfirmBuild()
    {
        //if the strucutre can be build and the player is tring to build it disable all the preview 
        //compoenets and enable the real ones and take the resources that it take to build the structure

        if (canBuild && Input.GetButtonDown("Fire1"))
        {
            //place build
            foreach (Transform child in preview_Obj.transform)
            {
                if (child.tag == "PreviewBuilding")
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
            resource_Holder.Oil -= OilCost;
            resource_Holder.Iron -= IronCost;
            OilCost = 0;
            IronCost = 0;
            preview_Obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            preview_Obj = null;
            isBuilding = false;
        }
    }

    //function that cancels the build and refunds the resources spend
    private void CancelBuild()
    {
        if (Input.GetButtonDown("Fire2") && preview_Obj != null || Input.GetButtonDown("Cancel") && preview_Obj != null)
        {
            //refund resources
            resource_Holder.Oil += OilCost;
            resource_Holder.Iron += IronCost;
            //reset cost
            IronCost = 0;
            OilCost = 0;
            //destroy preview
            Destroy(preview_Obj);
            preview_Obj = null;
            isBuilding = false;
        }
    }

    //spawn the structure depending on its ID
    public void SpawnStructure(int ID)
    {
        if (!isBuilding)
        {
            //set the cost of the structure
            CostTable(ID);

            if (resource_Holder.Iron >= IronCost && resource_Holder.Oil >= OilCost) // if the player has the resources spawn the structure
            {
                Instantiate(m_Loader.StructuresHolder[ID], transform.position, Quaternion.identity);
                isBuilding = true;
            }
            else
            {
                IronCost = 0;
                OilCost = 0;
            }
        }
    }

    //cost table on all structures
    private void CostTable(int tableId)
    {
        switch (tableId)
        {
            ///0 = iron
            case 0:
                IronCost = ironStructureCost[0];
                OilCost = ironStructureCost[1];
                break;
            ///1 = oil
            case 1:
                IronCost = oilStructureCost[0];
                OilCost = oilStructureCost[1];
                break;
            ///2 = turret
            case 2:
                IronCost = turretStructureCost[0];
                OilCost = turretStructureCost[1];
                break;
            ///3 = artilery
            case 3:
                IronCost = artileryStructureCost[0];
                OilCost = artileryStructureCost[1];
                break;
            ///4 = sniper
            case 4:
                IronCost = sniperStructureCost[0];
                OilCost = sniperStructureCost[1];
                break;

        }
    }
}
