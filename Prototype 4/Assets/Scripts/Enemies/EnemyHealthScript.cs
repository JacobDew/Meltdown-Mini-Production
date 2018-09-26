using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{

    private float m_fHealth = 2.0f;

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void TakeDamage(float _Damage)
    {
        m_fHealth -= _Damage;
        if (0.0001f > m_fHealth)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(float _Health)
    {
        m_fHealth = _Health;
    }
}
