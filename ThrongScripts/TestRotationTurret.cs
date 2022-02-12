using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    //rotation test script
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
