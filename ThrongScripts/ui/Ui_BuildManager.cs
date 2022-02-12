using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_BuildManager : MonoBehaviour
{
    private Text oilCount;
    private Text ironCount;
    private Text hpCount;
    private Stronghold strongHold;
    private Resource_Holder resource_Holder;

    private void Awake()
    {
        strongHold = GameObject.FindGameObjectWithTag("StrongHold").GetComponent<Stronghold>();
        resource_Holder = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Holder").GetComponent<Resource_Holder>();
        oilCount = transform.Find("OilHolder/OilCount").GetComponent<Text>();
        ironCount = transform.Find("IronHolder/IronCount").GetComponent<Text>();
        hpCount = transform.Find("HpHolder/HpCount").GetComponent<Text>();
        SetRecourceCount();
    }

    private void Update()
    {
        SetRecourceCount();
    }

    ///set the hp,oil,iron count text depending on how much the player currely has
    private void SetRecourceCount()
    {
        hpCount.text = strongHold.Health.ToString();
        oilCount.text = resource_Holder.Oil.ToString();
        ironCount.text = resource_Holder.Iron.ToString();
    }
}
