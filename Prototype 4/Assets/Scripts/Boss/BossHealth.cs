using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    private GameObject m_pBossHealth;

	// Use this for initialization
	void Start () {
        m_pBossHealth = GameObject.Find("BossHealthBar/Panel/Slider");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            TakeDamage(7.0f);
        }
    }

    void TakeDamage(float m_fDamage)
    {
        m_pBossHealth.GetComponent<Slider>().value = m_pBossHealth.GetComponent<Slider>().value - m_fDamage;
    }
}
