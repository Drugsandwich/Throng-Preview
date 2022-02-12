using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFleeState : EnemyBaseState_Giant
{
    public override void EnterState(Giants_Ai giantAi)
    {
        giantAi.AgentFlee(); // call the agent to flee
    }
}
