using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMiner : Miner
{
    private void Update()
    {
        Mining(false, true); // mine iron
    }
}
