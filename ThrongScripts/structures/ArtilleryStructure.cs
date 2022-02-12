using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryStructure : TurretLogic
{
    [SerializeField]
    private float y_Hight;
    [SerializeField]
    private Vector3 firstDestination;
    [SerializeField]
    private Transform lastDestination;
    private float fireRate = 1f;
    private float nextFireTime;
    private Resource_Loader projectal_Loader;
    private Transform shootPos;
    private GameObject shootFx;
    private Animator m_animator;
    private AudioSource audio_Source;
    private void Awake()
    {
        audio_Source = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        projectal_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
        shootPos = transform.Find("ShootPos");
        shootFx = transform.Find("Armature/main/ButtonCannon/TopCannon/ShootFx").gameObject;
    }

    private void Update()
    {
        GetTarget();//get the target to shoot at
        ShootMissle();
    }

    //function that checks if its time to shoot and if there is a target to shoot at
    private void ShootMissle()
    {
        //if there is a target and its the next time to shoot call the MissleCrodinates function ,enable  the shoot fx ,play the shoot animation and sound
        //set up next time to fire
        if(Time.time > nextFireTime && Target !=null)
        {
            MissleCordinates();
            nextFireTime = Time.time + 1f / fireRate;
            m_animator.SetTrigger("ArtileryShoot");
            StartCoroutine(ShootFX());
            audio_Source.PlayOneShot(audio_Source.clip);
        }
    }

    // function that will set the first and last desination for the rocket that will be spawned
    private void MissleCordinates()
    {
        firstDestination = Vector3.Lerp(this.transform.position, Target.transform.position, 0.5f); // calculate the first destination
        firstDestination.y = transform.position.y + y_Hight; // add the hight of the first desination for the rocket
        lastDestination = Target.transform;
        //projectal instanciate
        GameObject missleClone = Instantiate(projectal_Loader.ArtilleryMissle, shootPos.position, Quaternion.identity);
        Artilery_MissleProjectal missleCloneComp = missleClone.GetComponent<Artilery_MissleProjectal>();
        missleCloneComp.FirstDestination = firstDestination;//set the first desination for the rocket to follow
        missleCloneComp.LastDestination = lastDestination;//set the target as the last desination for the rocket to follow
    }

    IEnumerator ShootFX() //enable the shoot fx and disable it after small time 
    {
        shootFx.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        shootFx.SetActive(false);
    }
}
