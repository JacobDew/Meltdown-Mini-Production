using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCubeScript : MonoBehaviour
{
    private Vector3 m_vThisPos;
    private Vector3 m_vDirection;
    private GameObject m_pEffectCube;
    private int m_iMaterial;


    private int m_iMultiplier;
    private float m_fDelay;
    private float m_fDelayTimer;

    private float m_fLifetime;
    private float m_fTime;

    private float m_fDeltaTime = 0.0f;


	// Use this for initialization
	void Start()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        m_fDeltaTime = Time.deltaTime;

        if (m_fLifetime > m_fTime)
        {
            if (m_fDelay < m_fDelayTimer)
            {
                for (int iX = 0; iX < m_iMultiplier; iX++)
                {
                    GameObject TempPtr = Instantiate(m_pEffectCube);
                    TempPtr.AddComponent<CubeScript>().Initialize(Quaternion.Euler(Random.Range(-45.0f, 45.0f), Random.Range(-45.0f, 45.0f), Random.Range(-45.0f, 45.0f)),
                         m_vDirection, m_iMaterial);
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

        m_fDelayTimer += m_fDeltaTime;
        m_fTime += m_fDeltaTime;
    }

    public void Initialize(Vector3 _Position, Vector3 _Direction, int _Material, float _Lifetime = 0.2f, float _Delay = 0.01f, int _Multiplier = 3)
    {
        m_fTime = 0.0f;
        m_iMaterial = _Material;
        m_fLifetime = _Lifetime;
        m_fDelay = _Delay;
        m_iMultiplier = _Multiplier;
        m_vThisPos = _Position;
        m_vDirection = _Direction;
        m_pEffectCube = Resources.Load<GameObject>("EffectCube");
    }
}
