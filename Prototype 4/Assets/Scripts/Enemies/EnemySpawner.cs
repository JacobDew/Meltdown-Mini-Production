using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };


    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform PlayerEnemy;
        public Transform TowerEnemy;
        public int EnemyCount;
        public float SpawnRate;

    }

    public Wave[] Waves;
    public Text WaveDisplay;

    private int NextWave = 0;

    public Transform[] SpawnPoints;

    public float timeBetweenWave = 5f;
    public float WaveCountdown;

    private float SearchCountdown = 1f;

    public SpawnState State = SpawnState.COUNTING;

    

    void Start ()
    {
        WaveCountdown -= 0.3f;
        if (SpawnPoints.Length == 0)
        {
            Debug.Log("ERROR: No Spawnpoints refenced");

        }

        WaveCountdown = timeBetweenWave;


    }
    
    void Update()
    {
        if (State == SpawnState.WAITING)
        {
            //check if enemiesa re still alive
            if (!EnemeyIsAlive())
            {
                //Begin new wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (WaveCountdown <= 0)
        {
            if (State != SpawnState.SPAWNING)
            {
                //Stats spawning wave
                StartCoroutine(SpawnWave(Waves[NextWave]));

            }

        }

        else
        {
            WaveCountdown -= Time.deltaTime;

        }

        WaveDisplay.text = ("Wave: " + (NextWave + 1).ToString());
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        State = SpawnState.COUNTING;
        WaveCountdown = timeBetweenWave;

        if(NextWave + 1 > Waves.Length - 1)
        {
            NextWave = 0;
            Debug.Log("ALL WAVES COMPLETE!  Lopping...");
        }
        else
        {
            NextWave++;

        }
  
    }

    bool EnemeyIsAlive()
    {
        SearchCountdown -= Time.deltaTime;

        if(SearchCountdown <= 0f)
        {
            SearchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Follower").Length == 0)
            {

                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        State = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.EnemyCount; i++)
        {
            //30% of spawning Tower Enemey
            if(Random.Range(0,10) >= 4)
            {
                SpawnEnemy(_wave.TowerEnemy);
            }

            else
            {
                SpawnEnemy(_wave.PlayerEnemy);
            }
           
            yield return new WaitForSeconds(1f / _wave.SpawnRate);
        }



        State = SpawnState.WAITING;

        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

       

        Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
       
    }

}
