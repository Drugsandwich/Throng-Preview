using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantShieldState : EnemyBaseState_Giant
{
    private EnemyHealth shieldTarget;
    //enter state  of shield give 
    public override void EnterState(Giants_Ai giantAi)
    {
        giantAi.StopAgent();
        Collider[] hitCollider = new Collider[5];
        int colliderNumber = Physics.OverlapSphereNonAlloc(giantAi.transform.position, giantAi.Range, hitCollider, giantAi.M_Mask);//using non alloc to help the gorbage collector
        for (int i = 0; i < colliderNumber; i++)
        {
            if (hitCollider[i].tag == "Enemy")///if hit collider is a enemy then give it shield and enable heal fx on the target
            {
                shieldTarget = hitCollider[i].GetComponent<EnemyHealth>();
                if (shieldTarget != null)
                {
                    shieldTarget.Shield += giantAi.ShieldAmount;
                    giantAi.HpFx[i].SetActive(true);//the particle effect disables itself after it finishes
                    giantAi.HpFx[i].transform.position = shieldTarget.transform.position; // set position of the heal fx
                }
            }
        }
        giantAi.CurrentShieldT = giantAi.ShieldTime;//reset shield timer
    }
}
