using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    private int guardiansSpawn_count;
    public int GuardiansSpawn_count { get { return guardiansSpawn_count; } private set { } }

    private int giantsSpawn_count;
    public int GiantsSpawn_count { get { return giantsSpawn_count; } private set { } }

    private int bossSpawn_count;
    public int BossSpawn_count { get { return bossSpawn_count; } private set { } }

    [SerializeField] private float spawnRate;
    private float nextTime_ToSpawn;

    private float nextWave_CurretTime;
    private float nextWave_Time;

    private int guardiansSpawn_countTemp;
    private int giantsSpawn_countTemp;
    private int bossSpawn_countTemp;

    [SerializeField] private List<GameObject> waveEnemies = new List<GameObject>();
    private int emptyEnemies;

    [SerializeField] private Resource_Loader m_Loader;
    private bool isSpawning;

    private int waveCount;
    public int WaveCount { get { return waveCount; } private set { } }

    private void Start()
    {
        waveCount = 0;
        m_Loader = GameObject.FindGameObjectWithTag("Manager").transform.Find("Resource_Loader").GetComponent<Resource_Loader>();
    }

    private void Update()
    {
        SpawnWave();
        NextWaveChecker();
    }

    //Wave setter function
    private void SetWave()
    {
        waveCount++;//add wave count
        guardiansSpawn_count = waveCount * 3; // for every wave add 3 guardians

        if (guardiansSpawn_count > 10)
        {
            giantsSpawn_count = guardiansSpawn_count / 10; //every 10 guardian spawn 1 giant
        }

        if(giantsSpawn_count > 5)
        {
            bossSpawn_count = giantsSpawn_count / 5; // every 5 giant spawn 1 boss
        }

        guardiansSpawn_countTemp = guardiansSpawn_count;//set the guardian count to the temporary
        giantsSpawn_countTemp = giantsSpawn_count;//set the gaint count to the temporary
        bossSpawn_countTemp = bossSpawn_count;//set the boss count to the temporary
    }

    //Wave spawn function
    private void SpawnWave()
    {
        // if curret time is bigger then the next time to spawn and spawning is enabled spawn enmeis
        if (Time.time >= nextTime_ToSpawn && isSpawning)
        {
            nextTime_ToSpawn = Time.time + 1f / spawnRate;

            if(guardiansSpawn_countTemp > 0)//if guardian spawn count is bigger the zero spawn new guardian and add them to the list of the wave
            {
                GameObject guarianClone = Instantiate(m_Loader.Zealed_Guardian, transform.position, transform.rotation);
                waveEnemies.Add(guarianClone);
                guardiansSpawn_countTemp--; // remove one of the guardians from the spawn que int
            }

            if (giantsSpawn_countTemp > 0)//if giant spawn count is bigger the zero spawn new giant and add them to the list of the wave
            {
                GameObject giantClone = Instantiate(m_Loader.Zealed_Giant, transform.position, transform.rotation);
                waveEnemies.Add(giantClone);
                giantsSpawn_countTemp--;// remove one of the giant from the spawn que int
            }

            if(bossSpawn_countTemp > 0 && giantsSpawn_countTemp == 0)//if boss spawn count is bigger the zero and gaint spawn is done spawn new boss and add them to the list of the wave
            {
                GameObject bossClone = Instantiate(m_Loader.Zealed_Boss, transform.position, transform.rotation);
                waveEnemies.Add(bossClone);
                bossSpawn_countTemp--;// remove one of the boss from the spawn que int
            }
        }

        if (bossSpawn_countTemp == 0 && giantsSpawn_countTemp == 0 && guardiansSpawn_countTemp == 0)
        {
            SetWave();
            isSpawning = false;
        }
    }

    //check if the curret wave is dead if it is spawn next wave
    private void NextWaveChecker()
    {
        // for each enemy in the wave that is null add to emptyEnemies int
        for (int i = 0; i < waveEnemies.Count; i++)
        {
            if(waveEnemies[i] == null)
            {
                emptyEnemies++;
            }
        }

        //if empty enmies is equal to the wave count list clear the list and enable spwaning new wave
        if(emptyEnemies == waveEnemies.Count)
        {
            waveEnemies.Clear();
            isSpawning = true;
        }
        emptyEnemies = 0;// reset the int for next check
    }
}
