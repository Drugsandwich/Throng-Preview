using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : EnemyBaseState_Boss
{
    //enter state for the boss spawning
    public override void EnterState(Boss_Ai bossAi)
    {
        bossAi.StopAgent();
        bossAi.CurrentSpawnT -= Time.deltaTime;
        if (bossAi.CurrentSpawnT <= 0)// if the curret spawn time is less or equal to zero spawn guardians
        {
            for (int i = 0; i < bossAi.SpawnCount; i++)
            {
                MonoBehaviour.Instantiate(bossAi.M_Loader.Zealed_Guardian, bossAi.transform.position, Quaternion.identity);
            }

            bossAi.CurrentSpawnT = bossAi.SpawnTime; // reset timer
        }
    }
}
