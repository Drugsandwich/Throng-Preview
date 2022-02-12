using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilMiner : Miner
{
    private void Update()
    {
        Mining(true, false); // mine oil
    }
}
