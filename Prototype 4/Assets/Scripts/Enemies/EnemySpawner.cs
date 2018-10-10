using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    //  Positions outside of the visible portion game world.
    private Vector3 m_vBottomRight = new Vector3(92.2f, 1.0f, -46.9f);
    private Vector3 m_vTopRight = new Vector3(92.2f, 1.0f, 70.5f);
    private Vector3 m_vTopLeft = new Vector3(-51.1f, 1.0f, 70.5f);
    private Vector3 m_vBottomLeft = new Vector3(-51.1f, 1.0f, -46.9f);

    private GameObject[] m_pEnemyTypes;


    private GameObject m_pCurrentWave;
    //  Current Wave.
    private int m_iWaveNumber = 0;
    private int m_iLevel = 1;

    private const int m_iWaveMaxLv1 = 2;
    private const int m_iWaveMaxLv2 = 2;
    private const int m_iWaveMaxLv3 = 2;

    private bool m_bWaveActive = false;

    //  Delay between waves.
    private float m_fWaveTimer = 1.0f;
    private const float m_fWaveDelay = 1.0f;

    //  Number of enemies for current wave
    private int m_iEnemyCount = 0;
    private int m_iEnemyMax = 100;

    //  Timers for enemy spawning with max and min delay.
    private float m_fSpawnTimer = 0.0f;
    private float m_fSpawnRate = 0.1f;
    private const float m_fMaxSpawnRate = 0.05f;
    private const float m_fMinSpawnRate = 0.5f;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        m_pCurrentWave = GameObject.FindGameObjectWithTag("CurrentWave");
        m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_iWaveNumber.ToString();
        m_pEnemyTypes = new GameObject[] { Resources.Load<GameObject>("Enemey"), Resources.Load<GameObject>("TowerEnemey") };
    }
    
    void Update()
    {
        if (true == m_bWaveActive)
        {
            if (m_iEnemyMax <= m_iEnemyCount)
            {
                WaveCompleted();
            }
            else
            {
                SpawnEnemy();
            }

        }
        else if (0.0f < m_fWaveTimer)
        {
            m_fWaveTimer -= Time.deltaTime;
            Debug.Log(m_fWaveTimer);
            if (0.0f > m_fWaveTimer)
            {
                if (true == LevelComplete())
                {
                    //  Level transition.
                    m_iLevel += 1;
                    GoToLevel(m_iLevel);
                }
                else
                {
                    m_bWaveActive = true;
                    m_iWaveNumber += 1;
                    m_iEnemyCount = 0;
                    Debug.Log("Lv: " + m_iLevel + " Wv: " + m_iWaveNumber);
                    m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_iWaveNumber.ToString();
                }
            }
        }
        else
        {

        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        int EnemyCount = GameObject.FindGameObjectsWithTag("Follower").Length;
        Debug.Log(EnemyCount);
        if (0 == EnemyCount)
        {
            m_bWaveActive = false;
            m_fWaveTimer = m_fWaveDelay;
            //  Start Wave_Change Text animation.
        }
    }

    bool EnemyIsAlive()
    {
        m_fWaveTimer -= Time.deltaTime;

        if(m_fWaveTimer < 0.0f)
        {
            if (GameObject.FindGameObjectsWithTag("Follower").Length == 0)
            {

                return false;
            }
        }

        return true;
    }
    
    private void SpawnEnemy()
    {
        if (0.0f > m_fSpawnTimer)
        {
            m_fSpawnTimer = m_fSpawnRate;
            Vector3 vSpawnPosition;
            switch (Random.Range(0, 4))
            {
                case 0:
                    {
                        vSpawnPosition = m_vBottomRight - m_vTopRight;
                        vSpawnPosition = vSpawnPosition * Random.Range(0.0f, 1.0f);
                        vSpawnPosition = vSpawnPosition + m_vTopRight;
                        //  Get direction vector
                        //  Pick a point along the vector 0.0f to 1.0f
                        //  Add vector back
                    }
                    break;
                case 1:
                    {
                        vSpawnPosition = m_vTopRight - m_vTopLeft;
                        vSpawnPosition = vSpawnPosition * Random.Range(0.0f, 1.0f);
                        vSpawnPosition = vSpawnPosition + m_vTopLeft;
                    }
                    break;
                case 2:
                    {
                        vSpawnPosition = m_vTopLeft - m_vBottomLeft;
                        vSpawnPosition = vSpawnPosition * Random.Range(0.0f, 1.0f);
                        vSpawnPosition = vSpawnPosition + m_vBottomLeft;
                    }
                    break;
                case 3:
                    {
                        vSpawnPosition = m_vBottomLeft - m_vBottomRight;
                        vSpawnPosition = vSpawnPosition * Random.Range(0.0f, 1.0f);
                        vSpawnPosition = vSpawnPosition + m_vBottomRight;
                    }
                    break;
                default:
                    {
                        vSpawnPosition = m_vBottomRight - m_vTopRight;
                        vSpawnPosition = vSpawnPosition * Random.Range(0.0f, 1.0f);
                        vSpawnPosition = vSpawnPosition + m_vTopRight;
                    }
                    break;
            }
            GameObject Temp = Instantiate(m_pEnemyTypes[Random.Range(0, 2)]);
            Temp.transform.position = vSpawnPosition;
            m_iEnemyCount += 1;
        }
        m_fSpawnTimer -= Time.deltaTime;
    }

    private bool LevelComplete()
    {
        bool bComplete = false;
        switch (m_iWaveNumber)
        {
            case 1:
                {
                    if (m_iWaveMaxLv1 > m_iWaveNumber)
                    {
                        bComplete = false;
                    }
                    else
                    {
                        bComplete = true;
                    }
                }
                break;
            case 2:
                {
                    if (m_iWaveMaxLv2 > m_iWaveNumber)
                    {
                        bComplete = false;
                    }
                    else
                    {
                        bComplete = true;
                    }
                }
                break;
            case 3:
                {
                    if (m_iWaveMaxLv3 > m_iWaveNumber)
                    {
                        bComplete = false;
                    }
                    else
                    {
                        bComplete = true;
                    }
                }
                break;
            default:
                {
                    //  No Level.
                    bComplete = false;
                }
                break;
        }
        return bComplete;
    }

    private void GoToLevel(int _Level)
    {
        m_iWaveNumber = 0;
        m_fWaveTimer = m_fWaveDelay;
        switch (_Level)
        {
            case 0:
                {
                    SceneManager.LoadScene("Level 2");
                }
                break;
            case 1:
                {
                    SceneManager.LoadScene("Level 2");
                }
                break;
            case 2:
                {
                    SceneManager.LoadScene("Level 2");
                }
                break;
            case 3:
                {
                    SceneManager.LoadScene("Level 3");
                }
                break;
            default:
                {
                    SceneManager.LoadScene("Level 2");
                }
                break;
        }
        m_pCurrentWave = GameObject.FindGameObjectWithTag("CurrentWave");
        m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_iWaveNumber.ToString();
    }

}
