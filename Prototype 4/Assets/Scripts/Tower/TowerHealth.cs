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

        m_pHealthDisplay.transform.position = new Vector3(-4.5312f, 10.9f, 17.9f);
        m_pHealthDisplay.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().maxValue = m_fTowerHealth;
        m_pHealthDisplay.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().minValue = 0f;
        m_pHealthDisplay.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().value = m_fTowerHealth;
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
        m_pHealthDisplay.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().value = m_fTowerHealth;
    }


}
