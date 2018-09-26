using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector3 m_vDirection = new Vector3(0.0f, 0.0f, 0.0f);

    private float m_fSpeed;
    private float m_fDamage;
    private int m_iPierce;


	// Use this for initialization
	void Awake()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
		if (0.0f != m_vDirection.x || 0.0f != m_vDirection.z)
        {
            this.transform.position += m_vDirection * Time.deltaTime * m_fSpeed;
        }
	}

    //  When projectile goes offscreen it deletes itself.
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return m_fDamage;
    }

    public void Initialize(Vector3 _Direction, float _Damage, float _Speed, int _Pierce)
    {
       
    }
}