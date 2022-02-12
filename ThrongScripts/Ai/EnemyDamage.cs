using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //enemy damage to damge the stornghold
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }
}
