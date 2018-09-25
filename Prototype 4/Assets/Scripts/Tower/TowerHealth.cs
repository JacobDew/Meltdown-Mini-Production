using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerHealth : MonoBehaviour
{
    private float m_fTowerHealth;

    private GameObject m_pHealthDisplay;


	// Use this for initialization
	void Start()
    {
        m_pHealthDisplay = GameObject.FindGameObjectWithTag("TowerHealth");
        m_fTowerHealth = 2000.0f;
        m_pHealthDisplay.GetComponent<Text>().text = "Tower Health: " + m_fTowerHealth.ToString();
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void TakeDamage(float _Damage)
    {
        m_fTowerHealth -= _Damage;
        if (0.0f > m_fTowerHealth)
        {
            SceneManager.LoadScene("GameOver");
        }
        m_pHealthDisplay.GetComponent<Text>().text = "Tower Health: " + m_fTowerHealth.ToString();
    }
}
