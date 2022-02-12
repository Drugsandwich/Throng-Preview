using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WaveCounter : MonoBehaviour
{
    private EnemyWaveSpawner spawner;
    private GameObject bossHolder;
    private GameObject giantHolder;
    private GameObject guardianHolder;

    private Text bossCount;
    private Text giantCount;
    private Text guardianCount;
    private Text waveCount;

    //set up the Ui text and objects
    private void Awake()
    {
        bossHolder = transform.Find("Boss_Image").gameObject;
        bossCount = transform.Find("Boss_Image/BossCount").GetComponent<Text>();

        giantHolder = transform.Find("Giant_Image").gameObject;
        giantCount = transform.Find("Giant_Image/GiantCount").GetComponent<Text>();

        guardianHolder = transform.Find("Guardian_Image").gameObject;
        guardianCount = transform.Find("Guardian_Image/GuardianCount").GetComponent<Text>();

        waveCount = transform.Find("WaveCount").GetComponent<Text>();
        spawner = GameObject.FindGameObjectWithTag("EnemyPortal").GetComponent<EnemyWaveSpawner>();
    }

    //disable all of the spawnUI 
    private void Start()
    {
        bossHolder.SetActive(false);
        giantHolder.SetActive(false);
        guardianHolder.SetActive(false);
    }

    private void Update()
    {
        ChangeWaveCount();
    }

    private void ChangeWaveCount()
    {
        waveCount.text = spawner.WaveCount.ToString(); // change spawn count text depending on the wave 

        ///if there is more then 1 boss that will spawn enable the boss image
        if (spawner.BossSpawn_count > 0)
        {
            bossHolder.SetActive(true);
            bossCount.text = "x" + spawner.BossSpawn_count.ToString(); /// change the boss count depending on how many will spawn next
        }
        ///if there is more then 1 giant that will spawn enable the boss image
        if (spawner.GiantsSpawn_count > 0)
        {
            giantHolder.SetActive(true);
            giantCount.text = "x" + spawner.GiantsSpawn_count.ToString();/// change the giant count depending on how many will spawn next
        }

        ///if there is more then 1 giant that will spawn enable the boss image
        if (spawner.GuardiansSpawn_count > 0)
        {
            guardianHolder.SetActive(true);
            guardianCount.text = "x" + spawner.GuardiansSpawn_count.ToString();/// change the guanrdian count depending on how many will spawn next
        }
    }

}
