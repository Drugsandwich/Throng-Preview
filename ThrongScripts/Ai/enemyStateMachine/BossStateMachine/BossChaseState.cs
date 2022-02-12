using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : EnemyBaseState_Boss
{
    public override void EnterState(Boss_Ai bossAi)
    {
        bossAi.StartAgent(); // enable the agent to chase
    }
}
