using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector3 m_vDirection = new Vector3(0.0f, 0.0f, 0.0f);

    private float Speed;
    private float m_fDamage;


	// Use this for initialization
	void Awake()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
		if (0.0f != m_vDirection.x || 0.0f != m_vDirection.z)
        {
            this.transform.position += m_vDirection * Time.deltaTime * Speed;
        }
	}
    
    public void SetDirection(Vector3 _Direction, float _Damage, int _Type = 0, float _Speed = 20)
    {
        m_vDirection = _Direction;
        Speed = _Speed;
        m_fDamage = _Damage;
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
}