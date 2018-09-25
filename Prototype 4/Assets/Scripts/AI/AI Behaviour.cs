using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBehaviour : MonoBehaviour
{
    private GameObject[] m_Entities;
    private GameObject m_Player;

    private float m_fDamage;
    private float m_fDamageDelay;


	// Use this for initialization
	void Start()
    {
        m_Entities = GameObject.FindGameObjectsWithTag("Entities");
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_fDamage = 5.0f;
        m_fDamageDelay = 0.0f;

    }
	
	// Update is called once per frame
	void Update()
    {
        if (0.0f > m_fDamageDelay)
        {
            if (1.5f > Vector3.Distance(this.transform.position, m_Player.transform.position))
            {
                m_Player.GetComponent<Player>().TakeDamage(m_fDamage);
                m_fDamageDelay = 1.0f;
            }
        }
        m_fDamageDelay -= Time.deltaTime;
    }
}
