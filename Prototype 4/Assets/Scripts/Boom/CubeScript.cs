using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Vector3 m_vDirection;
    private float m_fSpeed;
    private float m_fDecay;


    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		if (0.01f < m_fSpeed)
        {
            this.transform.Translate(m_vDirection * m_fSpeed * Time.deltaTime);
            this.transform.localScale *= Time.deltaTime + 1.0f;
        }
        else
        {
            Destroy(gameObject);
        }
        m_fSpeed *= m_fDecay;
	}

    public void Initialize(Quaternion _Rotation, float _Speed = 20.0f, float _Decay = 0.5f)
    {
        m_vDirection = new Vector3(0.0f, 1.0f, 0.0f);
        this.transform.rotation = _Rotation;
        m_fSpeed = _Speed;
        m_fDecay = _Decay;
    }
}
