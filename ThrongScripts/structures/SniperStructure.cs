using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperStructure : TurretLogic
{
    private float fireRate = 0.3f;
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
        m__Mask = LayerMask.GetMask("Giants", "Enemy");// set layers for raycast and prevent constant checking for anthing else but the given layers
        TurretHolder = transform.Find("TurretHolder").gameObject;
        shootPos = TurretHolder.transform.Find("ShootPos");
        bloodFx = transform.Find("BloodFxObj").gameObject;
        shootFx = transform.Find("TurretHolder/SniperStructureSniperBarrleMesh/ShootFx").gameObject;
    }

    private void Update()
    {
        Functions();
    }

    //master function
    private void Functions()
    {
        GetTarget();//get target to shoot
        RotateTurret();//rotate the structure turret

        if (Time.time > nextFireTime && Target != null) // if there is a target
        {
            ShootRaycast();
            StartCoroutine(ShootFX());
            nextFireTime = Time.time + 1f / fireRate;
            m_Anim.SetBool("isFiring", true);
            audio_Source.PlayOneShot(audio_Source.clip);
        }
        else
        {
            m_Anim.SetBool("isFiring", false);
        }
    }

    /// shooot ray from the shootpos 
    private void ShootRaycast()
    {
        shootPos.rotation = Quaternion.LookRotation(Target.transform.position - shootPos.position); // rotate the shoo position to look at the curret target
        RaycastHit hit;
        if (Physics.Raycast(shootPos.position, shootPos.forward, out hit, Mathf.Infinity, m__Mask)) // if the ray hits the enemy damage it and set the blood fx to true
        {
            EnemyHealth targetHp = hit.transform.GetComponent<EnemyHealth>();
            if (targetHp != null)// check if the target has the hp componenet
            {
                targetHp.TakeDamage(damage);
                bloodFx.transform.position = hit.point;
                bloodFx.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                bloodFx.SetActive(true);
            }
        }
    }

    IEnumerator ShootFX() // enable and disable the shoot fx after small timer
    {
        shootFx.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        shootFx.SetActive(false);
    }
}
