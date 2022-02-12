using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int shield;
    public int Shield { get { return shield; } set { shield = value; } }

    [SerializeField]
    private int health;
    public int Health { get { return health; } set { health = value; } }

    private int startHp;
    public int StartHp { get { return startHp; } set { startHp = value; } }

    private Resource_Loader m_Loader;

    private void Start()
    {
        startHp = health;
        m_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
    }

    //take given damege and calculate it between the shield and health
    public void TakeDamage(int damage)
    {
        int damageLeft = damage - shield; // calculate damage left 

        if(shield > 0)
        {
            shield -= damage;
        }

        if(damageLeft > 0)
        {
            health -= damageLeft;
        }

        if(health <= 0) // if dead spawn blood fx
        {
            Instantiate(m_Loader.BloodExplosion_fx, transform.position, Quaternion.identity); // spawn blood fx
            Destroy(gameObject);
        }
    }
}
