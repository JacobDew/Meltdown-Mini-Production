using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float m_fTimer;
    private float m_fDeath;


	// Use this for initialization
	void Start()
    {
        m_fTimer = 0.0f;
        m_fDeath = Random.Range(0.15f, 0.4f);
    }
	
	// Update is called once per frame
	void Update()
    {
        m_fTimer += Time.deltaTime;
        this.transform.localScale = (1.0f + (Time.deltaTime * 3.0f)) * this.transform.localScale;
        if (m_fDeath < m_fTimer)
        {
            Destroy(gameObject);
        }
	}
}
