using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float m_fLifetime;


	// Use this for initialization
	void Start()
    {
        m_fLifetime = Random.Range(0.15f, 0.4f);
    }
	
	// Update is called once per frame
	void Update()
    {
        m_fLifetime -= Time.deltaTime;
        this.transform.localScale = (1.0f + (Time.deltaTime * 3.0f)) * this.transform.localScale;
        if (m_fLifetime < 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
