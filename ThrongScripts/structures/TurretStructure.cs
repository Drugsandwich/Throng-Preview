using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStructure : TurretLogic
{
    private float fireRate = 20f;
    private float nextFireTime;
    private Transform shootPos;
    private GameObject shootFx;
    private GameObject bloodFx;
    private LayerMask m__Mask; /// it has double underscore because error will pop up if you use a layermask with the same name from inheritance
    [SerializeField]
    private int damage;
    private Animator m_Anim;
    private AudioSource audio_Source;

    private void Awake()
    {
        audio_Source = GetComponent<AudioSource>();
        m_Anim = GetComponent<Animator>();
        m__Mask = LayerMask.GetMask("Giants", "Enemy");// set layers for raycast and prevent constant checking for tags
        TurretHolder = transform.Find("TurretHolder").gameObject;
        shootPos = TurretHolder.transform.Find("ShootPos");
        bloodFx = transform.Find("BloodFxObj").gameObject;
        shootFx = transform.Find("TurretHolder/miniGunMesh/ShootFx").gameObject;
    }

    private void Update()
    {
        Functions();
    }

    private void Functions()
    {
        GetTarget(); // get the target
        RotateTurret();//rotate the turret
        audio_Source.enabled = (Target != null); // enable or disable audio source that will play the shooting sound on awake depending if there is a target or not

        // if there is a target and its the next time to fire execute the shootray fucntion , enable shootFx, set next time to fire and play animation
        if (Time.time > nextFireTime && Target != null)
        {
            ShootRaycast();
            shootFx.SetActive(true);
            nextFireTime = Time.time + 1f / fireRate;
            m_Anim.SetBool("isFiring", true);
        }
        else if(Target == null) // if there is no target disable blood and shoot fx and stop firing animation
        {
            m_Anim.SetBool("isFiring", false);
            bloodFx.SetActive(false);
            shootFx.SetActive(false);
        }
    }

    //shoot raycast at the target position
    private void ShootRaycast()
    {
        shootPos.rotation = Quaternion.LookRotation(Target.transform.position - shootPos.position); // rotate the shoot position to the enemy
        RaycastHit hit;
        if(Physics.Raycast(shootPos.position,shootPos.forward,out hit, Mathf.Infinity, m__Mask))// if the raycast hits the layers given that are only enemies hurt the enemy
        {
            EnemyHealth targetHp = hit.transform.GetComponent<EnemyHealth>();
            if(targetHp != null)//if the target has hp damage it and actiavte the blood fx
            {
                targetHp.TakeDamage(damage);
                bloodFx.transform.position = hit.point;
                bloodFx.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); // rotate the blood fx 
                bloodFx.SetActive(true);
            }
        }
    }

}
