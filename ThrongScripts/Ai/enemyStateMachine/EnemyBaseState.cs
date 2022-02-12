using UnityEngine;

//base state for the giant ai
public abstract class EnemyBaseState_Giant
{
    public abstract void EnterState(Giants_Ai giantAi);

}

//base state for the boss ai
public abstract class EnemyBaseState_Boss
{
    public abstract void EnterState(Boss_Ai bossAi);

}
