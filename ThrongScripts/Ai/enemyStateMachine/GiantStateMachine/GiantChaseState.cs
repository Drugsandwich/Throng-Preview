using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantChaseState : EnemyBaseState_Giant
{
    public override void EnterState(Giants_Ai giantAi)
    {
        giantAi.StartAgent();//start the agentto chase
    }
}
