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

    private GameObject m_pWaveCompleteText;
    private GameObject m_pCurrentWave;

    //  Current Wave.
    private float m_f60 = 0.0f;

    public int m_iWaveNumber = 0;
    private int m_iLevel = 1;

    private const int m_iWaveMaxLv1 = 11;
    private const int m_iWaveMaxLv2 = 11;
    private const int m_iWaveMaxLv3 = 0;

    private bool m_bWaveActive = false;

    //  Delay between waves.
    private float m_fWaveTimer = 1.0f;
    private const float m_fWaveDelay = 8.0f;

    //  Number of enemies for current wave
    private int m_iEnemyCount = 0;
    private int m_iEnemyMax = 30;

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
        m_pWaveCompleteText = GameObject.Find("WaveComplete");
        if (null != m_pWaveCompleteText)
        {
            m_pWaveCompleteText.SetActive(false);
        }
    }
    
    void Update()
    {
        if (null == m_pCurrentWave)
        {
            m_pCurrentWave = GameObject.FindGameObjectWithTag("CurrentWave");

        }


        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }


        if (3 > m_iLevel)
        {
            m_f60 = 0.0167f / Time.deltaTime;
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
                if (4.0f > m_fWaveTimer)
                {
                    if (null == m_pWaveCompleteText)
                    {
                        m_pWaveCompleteText = GameObject.Find("WaveComplete");
                    }
                    m_pWaveCompleteText.SetActive(false);
                }
                m_fWaveTimer -= Time.deltaTime;
                //Debug.Log(m_fWaveTimer);
                if (0.0f > m_fWaveTimer)
                {
                    m_bWaveActive = true;
                    m_iWaveNumber += 1;
                    m_iEnemyCount = 0;
                    if (null != m_pCurrentWave)
                    {
                        //Debug.Log("Lv: " + m_iLevel + " Wv: " + m_iWaveNumber);
                        m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_iWaveNumber.ToString();
                    }
                }
            }
            else
            {

            }
            // m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_f60.ToString();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void WaveCompleted()
    {
        if (null != GameObject.FindGameObjectWithTag("Player"))
        {
            if (0 == GameObject.FindGameObjectsWithTag("Follower").Length)
            {
                if (null == m_pWaveCompleteText)
                {
                    m_pWaveCompleteText = GameObject.Find("WaveComplete");
                }
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Wave Complete");
                m_pWaveCompleteText.GetComponent<Text>().text = "Wave " + m_iWaveNumber.ToString() + " Complete!";
                m_pWaveCompleteText.SetActive(true);
                m_bWaveActive = false;
                m_fWaveTimer = m_fWaveDelay;
                //  Start Wave_Change Text animation.
            }
        }
        else
        {
            return;
        }
    }

    bool EnemyIsAlive()
    {
        m_fWaveTimer -= Time.deltaTime;

        if(m_fWaveTimer < 0.0f)
        {
            if (0 == GameObject.FindGameObjectsWithTag("Follower").Length)
            {

                return false;
            }
        }

        return true;
    }
    
    public void SetWaveNumber(int _Wave)
    {
        m_iWaveNumber = _Wave;
    }

    public void WaveZero()
    {
        m_iWaveNumber = 0;
    }

    private void SpawnEnemy()
    {
        if (11 == m_iWaveNumber)
        {
            return;
        }
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
            int iRand = 0;
            if (2 == m_iLevel)
            {
                iRand = 0;
            }
            else
            {
                iRand = Random.Range(0, 2);
            }
            GameObject Temp = Instantiate(m_pEnemyTypes[iRand]);
            Temp.transform.position = vSpawnPosition;
            Temp.tag = "Follower";
            m_iEnemyCount += 1;
        }
        m_fSpawnTimer -= Time.deltaTime;
    }

    public bool LevelComplete()
    {
        bool bComplete = false;
        switch (m_iWaveNumber)
        {
            case 1:
                {
                    if (m_iWaveMaxLv1 <= m_iWaveNumber && false == m_bWaveActive)
                    {
                        bComplete = true;
                    }
                    else
                    {
                        bComplete = false;
                    }
                }
                break;
            case 2:
                {
                    if (m_iWaveMaxLv2 <= m_iWaveNumber && false == m_bWaveActive)
                    {
                        bComplete = true;
                    }
                    else
                    {
                        bComplete = false;
                    }
                }
                break;
            case 3:
                {
                    if (m_iWaveMaxLv3 <= m_iWaveNumber && false == m_bWaveActive)
                    {
                        bComplete = true;
                    }
                    else
                    {
                        bComplete = false;
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

    public void GoToLevel(int _Level)
    {
        m_iLevel = _Level;
        m_iWaveNumber = 0;
        m_fWaveTimer = m_fWaveDelay;
        switch (m_iLevel)
        {
            case 0:
                {
                    //SceneManager.LoadScene("Main");
                }
                break;
            case 1:
                {
                   // SceneManager.LoadScene("Level 1");
                }
                break;
            case 2:
                {
                   // SceneManager.LoadScene("Level 2");
                }
                break;
            case 3:
                {
                   // SceneManager.LoadScene("Level 3");
                }
                break;
            default:
                {
                    //SceneManager.LoadScene("Level 1");
                }
                break;
        }
        m_pCurrentWave = GameObject.FindGameObjectWithTag("CurrentWave");
        if (null != m_pCurrentWave)
        {
            m_pCurrentWave.GetComponent<Text>().text = "Wave: " + m_iWaveNumber.ToString();
        }
    }

    public void Death()
    {
        m_iWaveNumber = 0;
        m_iLevel = 1;
        m_iEnemyCount = 0;
        m_bWaveActive = false;
    }

    public int GetMaxWave()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            return m_iWaveMaxLv1;
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            return m_iWaveMaxLv2;
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            return m_iWaveMaxLv3;
        }


        return 0;
    }
}
