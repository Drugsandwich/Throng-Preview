using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Ai : AImovement
{
    [SerializeField]private Resource_Loader m_Loader;
    public Resource_Loader M_Loader { get { return m_Loader; } set { m_Loader = value; } }

    [SerializeField] private float currentSpawnT;
    public float CurrentSpawnT { get { return currentSpawnT; } set { currentSpawnT = value; } }
    [SerializeField] private float spawnTime;
    public float SpawnTime { get { return spawnTime; } set { spawnTime = value; } }

    [SerializeField] private int spawnCount;
    public int SpawnCount { get { return spawnCount; }  set { spawnCount = value; } }

    private EnemyHealth enemyHp;

    private EnemyBaseState_Boss curretState;
    private readonly BossChaseState chaseState = new BossChaseState();
    private readonly BossSpawnState spawnState = new BossSpawnState();
    private readonly BossFleeState fleeState = new BossFleeState();

    private void Start()
    {
        m_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
        currentSpawnT = spawnTime;
        enemyHp = GetComponent<EnemyHealth>();
    }


    private void Update()
    {
        StateCondition();
    }

    ///function that changes the states of the boss
    private void StateCondition()
    {
        currentSpawnT -= Time.deltaTime;
        if (currentSpawnT <= 0) // if spawn time is less or equal to zero change to spawn state
        {
            TransitionToState(spawnState);
        }

        if (enemyHp.Health > enemyHp.StartHp / 2) // if boss has more then 1/2 of his hp go to chase state
        {
            TransitionToState(chaseState);
        }
        else // else go to flee state
        {
            TransitionToState(fleeState);
        }
    }

    // change the curret state and execute the enterState
    public void TransitionToState(EnemyBaseState_Boss state)
    {
        curretState = state;
        curretState.EnterState(this);
    }
}
