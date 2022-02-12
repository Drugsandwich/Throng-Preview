using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stronghold : Miner
{
  [SerializeField] private int health;
    public int Health { get { return health; } set { health = value; } }

    private bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    private GameObject fxFire;

    private void Start()
    {
        fxFire = transform.Find("fxFireDead").gameObject;
        health = 100;
    }

    private void Update()
    {
        Mining(true, true);
    }

    //damage stronghold and destoy target(enemy)
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            health -= enemy.Damage;
            Destroy(enemy.gameObject);

            if (health <= 0)
            {
                fxFire.SetActive(true);
                isDead = true;
                health = 0;
            }
        }
    }
}
