using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private GameObject turretHolder;
    public GameObject TurretHolder { get { return turretHolder; } set { turretHolder = value; } }
    private Vector3 rotVec;
    [SerializeField]
    private GameObject target;
    public GameObject Target { get { return target; } private set { } }
    private LayerMask m_Mask;
    [SerializeField]
    private float targetDistance;
    public float TargetDistance { get { return targetDistance; } private set { } }
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private Vector3 minClampRot;
    [SerializeField]
    private Vector3 maxClampRot;
    private Vector3 clampRotVec;

    private void Start()
    {
        m_Mask = LayerMask.GetMask("Giants", "Enemy");
    }

    //Function that get a target if there is no target 
    //and if there is a target then calculate the distance from the gameobject that i calling that function to the target
    public void GetTarget()
    {
        if(target == null) // if there is no target try get a new target
        {
            Collider[] hitCollider = new Collider[1];
            // get the collider numbers by using non alloc OverlapSphere (because it does not generate garbage to collect)
            int colliderNumber = Physics.OverlapSphereNonAlloc(this.transform.position, radius, hitCollider,m_Mask);
            for (int i = 0; i < colliderNumber; i++)
            {
                //check if collider is a enemy if yes asssign it as curret target and break out of the loop
                if (hitCollider[i].tag == "Enemy") // if the collider is a enemy then set up the main target
                {
                    target = hitCollider[i].gameObject;
                    break; //exit the loop
                }
            }
        }
        else // else call the GetDistance function
        {
            GetDistance();
        }
    }

    //function that rotates the turret holder to look at the target (if it exists) its also clamped for models that need clamping to not clip through
    public void RotateTurret()
    {
        if(target != null)
        {
            rotVec = target.transform.position - turretHolder.transform.position;
            turretHolder.transform.rotation = Quaternion.LookRotation(rotVec);
            clampRotVec.x = Mathf.Clamp(turretHolder.transform.eulerAngles.x, minClampRot.x, maxClampRot.x);
            clampRotVec.y = turretHolder.transform.eulerAngles.y;
            clampRotVec.z = Mathf.Clamp(turretHolder.transform.eulerAngles.z, minClampRot.z, maxClampRot.z);
            turretHolder.transform.eulerAngles = clampRotVec;
        }
    }

    //get the distance from the gameobject that is calling this function and the current target
    private void GetDistance()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if(targetDistance >= maxDistance) // if the target distance is more then the max distance make the curret target null as its out of range
        {
            target = null;
        }
    }
}
