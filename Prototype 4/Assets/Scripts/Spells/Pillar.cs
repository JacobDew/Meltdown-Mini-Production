using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    GameObject m_pCube;

    private Vector3 m_vPosition;
    private float m_fRadius = 0.5f;

    private Vector3 m_vUp = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 m_vDirection = new Vector3(-0.7071f, 0.0f, 0.7071f);
    private float m_fDrawRange = 180.0f;

    private float m_fLifetime = 20.0f;

    private float m_fTimer = 0.0f;
    private const float m_fDelay = 0.019f;

    private float m_fDeltaTime = 0.0f;


	// Use this for initialization
	void Start ()
    {
        if (null == m_vPosition)
        {
            this.gameObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        }
        m_pCube = Resources.Load<GameObject>("EffectCube");
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_fDeltaTime = Time.deltaTime;
        GameObject TempPtr;
        if (m_fDelay < m_fTimer)
        {
            Debug.Log(m_fTimer);
            for (float fX = 0.0f; fX < m_fDrawRange; fX += 15.5f)
            {
                TempPtr = Instantiate(m_pCube);
                TempPtr.transform.position = this.transform.position;
                TempPtr.transform.Rotate(m_vUp, -fX);
                TempPtr.transform.localScale *= 1.5f;
                TempPtr.transform.Translate(m_vDirection * m_fRadius);
                TempPtr.AddComponent<CubeScript>().Initialize(-m_vUp, Random.Range(1.0f, 3.5f), 0.5f, 1, 2.0f);
            }
            m_fTimer = 0.0f;
        }
        m_fTimer += m_fDeltaTime;
        m_fLifetime -= m_fDeltaTime;
        if (0.0001f > m_fLifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetPos(Vector3 _Position, float _Lifetime = 5.0f)
    {
        m_vPosition = _Position;
        m_fLifetime = _Lifetime;
    }
}
