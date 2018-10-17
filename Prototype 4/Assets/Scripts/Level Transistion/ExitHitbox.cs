using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHitbox : MonoBehaviour {

    private GameObject m_pSpawner;
    private Animator m_DoorAnimator;
    private GameObject m_pLevelChanger;

    private bool IsOpened = false;
    private bool bFirst = true;

    // Use this for initialization
    void Start () {
        m_pSpawner = GameObject.Find("SpawnerControl");
        m_pLevelChanger = GameObject.Find("Fade Canvas");
        m_DoorAnimator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (null == m_pSpawner)
        {
            m_pSpawner = GameObject.Find("SpawnerControl");
        }
        else if (m_pSpawner.GetComponent<EnemySpawner>().m_iWaveNumber >= m_pSpawner.GetComponent<EnemySpawner>().GetMaxWave() && bFirst)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Door");
            m_DoorAnimator.SetTrigger("OpenDoor");
            IsOpened = true;
            bFirst = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (other.tag == "Player" && IsOpened)
            {
                m_pLevelChanger.GetComponent<LevelChanger>().FadeToLevel("Level 2");
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Swap("Level 1", "Level 2", 20.0f);

            }
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (other.tag == "Player" && IsOpened)
            {
                m_pLevelChanger.GetComponent<LevelChanger>().FadeToLevel("Level 3");
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Swap("Level 2", "Boss Level", 20.0f);

            }
        }

    }
}
