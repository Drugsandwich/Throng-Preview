using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFleeState : EnemyBaseState_Boss
{
    public override void EnterState(Boss_Ai bossAi)
    {
        bossAi.AgentFlee();//call the agent to flee
    }
}
