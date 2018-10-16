using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Vector3 m_vForward = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 m_vBias = new Vector3(0.0f, 0.0f, 0.0f);

    private float m_fSpeed = 7.0f;
    private float m_fBiasSpeed = 0.0f;

    private float m_fLifeTimer;
    private float m_fLifeTime = 3.0f;

    private float m_fDeltaTime = 0.0f;


	// Use this for initialization
	void Start()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
        m_fDeltaTime = Time.deltaTime;
        this.transform.Translate(m_vForward * m_fSpeed * m_fDeltaTime);
        this.transform.position += m_vBias * m_fBiasSpeed * m_fDeltaTime;
        m_fLifeTimer += m_fDeltaTime;
        if (m_fLifeTime < m_fLifeTimer)
        {
            Destroy(this.gameObject);
        }
	}

    public void Initialize(Vector3 _Forward, Vector3 _Bias, float _BiasSpeed)
    {
        m_vForward = _Forward;
        m_vBias = _Bias;
        m_fBiasSpeed = _BiasSpeed;
        this.gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    public void Initialize(Vector3 _Forward)
    {
        m_vForward = _Forward;
        this.gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(m_fSpeed * 2.0f);
            Destroy(this.gameObject);
        }
    }


}
