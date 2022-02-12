using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artilery_MissleProjectal : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private LayerMask m_Mask;
    private EnemyHealth target;
    [SerializeField]
    private int damage;

    private Vector3 firstDestination;
    public Vector3 FirstDestination { get { return firstDestination; } set { firstDestination = value; } }
    private Transform lastDestination;
    public Transform LastDestination { get { return lastDestination; } set { lastDestination = value; } }
    private Vector3 rotVec;

    private float distance;

    [SerializeField]
    private float missleSpeed;
    private bool reached_FirstDestination;
    private Resource_Loader m_Loader;

    private void Awake()
    {
        m_Mask = LayerMask.GetMask("Giants", "Enemy");
        m_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
    }

    private void Update()
    {
        Functions();
    }

    private void Functions()
    {
        if (lastDestination != null) // execute follow target if the target(last destination) exists
        {
            FollowTarget();
        }
        else //else explode
        {
            Explode();
        }
    }

    //function that follows the given target
    private void FollowTarget()
    {
        // if first destination is not reached move to the first position given
        if (!reached_FirstDestination)
        {
            distance = Vector3.Distance(this.transform.position, firstDestination); ///  calculate distnace between target and curret possition
            rotVec = firstDestination - transform.position;
            transform.rotation = Quaternion.LookRotation(rotVec); //rotate the rocket looking at the destination
            transform.position = Vector3.MoveTowards(transform.position, firstDestination, missleSpeed * Time.deltaTime); // move forward to first destination

            if(distance <= 3f) // if distance is less or equal to 3 the first destination is reached
            {
                reached_FirstDestination = true;
            }
        }
        else
        {
            distance = Vector3.Distance(this.transform.position, lastDestination.position);// calculate distance between rocket and actual target(last destination)
            rotVec = lastDestination.position - transform.position;
            transform.rotation = Quaternion.LookRotation(rotVec);//rotate the rocket looking at the last destination
            transform.position = Vector3.MoveTowards(transform.position, lastDestination.position, missleSpeed * Time.deltaTime);// move forward to last destination

            if (distance <= 2f)// if distance is less or equal to 2 explode
            {
                Explode();
            }
        }
    }

    //explosion function that spawn explosion fx and gets all the enemies and damages them
    private void Explode()
    {
        Instantiate(m_Loader.ArtilleryMissle_fx, transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hitCol in hitColliders)
        {
            if (hitCol.transform.tag == "Enemy")// if the collider is a enemy dmaage him
            {
                target = hitCol.transform.GetComponent<EnemyHealth>();
                if (target != null) // if the collider has the hp componenet make it take damage
                {
                    target.TakeDamage(damage);
                }
            }
        }
        Destroy(this.gameObject); // desotry rock the Explosion fx deletes itself with the particle system
    }
}
