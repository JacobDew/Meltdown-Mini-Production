using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCubeScript : MonoBehaviour
{
    private Vector3 m_vThisPos;
    private GameObject m_pEffectCube;

    private int m_iMultiplier;
    private float m_fDelay;
    private float m_fDelayTimer;

    private float m_fLifetime;
    private float m_fTime;


	// Use this for initialization
	void Start()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        if (m_fLifetime > m_fTime)
        {
            if (m_fDelay < m_fDelayTimer)
            {
                for (int iX = 0; iX < m_iMultiplier; iX++)
                {
                    GameObject TempPtr = Instantiate(m_pEffectCube);
                    TempPtr.AddComponent<CubeScript>().Initialize(Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
                    TempPtr.transform.position = m_vThisPos;
                    //TempPtr.GetComponent<CubeScript>().Initialize(Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
                }
                m_fDelayTimer = 0.0f;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        m_fDelayTimer += Time.deltaTime;
        m_fTime += Time.deltaTime;
    }

    public void Initialize(Vector3 _Position, float _Lifetime = 0.3f, float _Delay = 0.01f, int _Multiplier = 5)
    {
        m_fTime = 0.0f;
        m_fLifetime = _Lifetime;
        m_fDelay = _Delay;
        m_iMultiplier = _Multiplier;
        m_vThisPos = _Position;
        m_pEffectCube = Resources.Load<GameObject>("EffectCube");
    }
}
