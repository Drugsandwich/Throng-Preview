using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField]
    private float miningTime;
    private float currentMining_Time;
    [SerializeField]
    private int miningAmount;
    private Resource_Holder resource_Holder;

    private void Awake()
    {
        currentMining_Time = miningTime;
        resource_Holder = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Holder").GetComponent<Resource_Holder>();
    }

    // mine the resources depending on the passed arguments
    public void Mining(bool miningOil,bool miningIron)
    {
        currentMining_Time -= Time.deltaTime;
        if (currentMining_Time <= 0) // if its time to get the resources add new resources to the resource holder
        {
            if (miningIron) // if its mining iron add iron
            {
                resource_Holder.Iron += miningAmount;
            }

            if (miningOil) // if its mining oil add oil
            {
                resource_Holder.Oil += miningAmount;
            }
            currentMining_Time = miningTime; // reset mine time
        }
    }
}
