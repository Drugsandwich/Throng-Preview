using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giants_Ai : AImovement
{
    [SerializeField] private float currentShieldT;
    public float CurrentShieldT { get { return currentShieldT; } set { currentShieldT = value; } }
    [SerializeField] private float shieldTime;
    public float ShieldTime { get { return shieldTime; } set { shieldTime = value; } }

    [SerializeField] private int shieldAmount;
    public int ShieldAmount { get { return shieldAmount; } set { shieldAmount = value; } }

    [SerializeField] private float range;
    public float Range { get { return range; } set { range = value; } }
    private LayerMask m_Mask;
    public LayerMask M_Mask { get { return m_Mask; } set { range = m_Mask; } }
    private EnemyBaseState_Giant curretState;
    [SerializeField]private GameObject[] hpFx = new GameObject[5];
    public GameObject[] HpFx { get { return hpFx; } private set { } }

    private EnemyHealth enemyHp;

    private readonly GiantShieldState shieldState = new GiantShieldState();
    private readonly GiantChaseState chaseState = new GiantChaseState();
    private readonly GiantFleeState fleeState = new GiantFleeState();

    private void Start()
    {
        enemyHp = GetComponent<EnemyHealth>();
        SetUpFx();
        currentShieldT = shieldTime;
        m_Mask = LayerMask.GetMask("Giants");
        m_Mask = ~m_Mask;//this layer will be ignored (Giants)
        TransitionToState(chaseState);
        TransitionToState(shieldState);
    }

    private void Update()
    {
        StateCondition();
    }

    private void StateCondition()
    {
        currentShieldT -= Time.deltaTime;
        if(currentShieldT <= 0) // if the curret  shield time is less or equal to zero go to shield give state
        {
            TransitionToState(shieldState);
        }

        if (enemyHp.Health > enemyHp.StartHp / 2) // if giant has more then 1/2 of his hp go to chase state
        {
            TransitionToState(chaseState);
        }
        else // else go to flee state
        {
            TransitionToState(fleeState);
        }
    }

    //transition state to the given state and execute the EnterState of the current state
    public void TransitionToState(EnemyBaseState_Giant state)
    {
        curretState = state;
        curretState.EnterState(this);
    }

    //get all the Heal fx objects and assign them to array of object
    private void SetUpFx()
    {
        for(int i = 0; i < hpFx.Length; i++)
        {
            hpFx[i] = transform.Find("HpFx" + i).gameObject;
        }
    }
}
