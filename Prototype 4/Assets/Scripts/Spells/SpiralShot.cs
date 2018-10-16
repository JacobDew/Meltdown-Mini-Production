using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShot : MonoBehaviour
{
    private GameObject m_pEffectCubeX;

    private float m_fRotationSpeed = 20.0f;
    private Vector3 m_vRotationAxis = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 m_vForwardVector = new Vector3(-0.7071f, 0.0f, -0.7071f);

    private float m_fProjectileTimer = 0.0f;
    private float m_fProjectileDelay = 0.55f;
    private float m_fSpeed = 5.0f;

    //  Deltatime variable to save processing.
    private float m_fDeltaTime = 0.0f;

	// Use this for initialization
	void Start()
    {
        m_pEffectCubeX = Resources.Load<GameObject>("EffectCubeZ");
    }
	
	// Update is called once per frame
	void Update()
    {
        m_fDeltaTime = Time.deltaTime;
        this.transform.position += m_vForwardVector * m_fSpeed * m_fDeltaTime;
        this.transform.Rotate(m_vRotationAxis, m_fRotationSpeed * m_fDeltaTime);
        m_fProjectileTimer += m_fDeltaTime;
        if (m_fProjectileDelay < m_fProjectileTimer)
        {
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(1.0f, 0.0f, 0.0f), m_vForwardVector, m_fSpeed);
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(-1.0f, 0.0f, 0.0f), m_vForwardVector, m_fSpeed);
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(0.0f, 0.0f, 1.0f), m_vForwardVector, m_fSpeed);
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(0.0f, 0.0f, -1.0f), m_vForwardVector, m_fSpeed);
            m_fProjectileTimer = 0.0f;
        }
	}

    public void SetDirection(Vector3 _Forward)
    {
        m_vForwardVector = _Forward;
    }
}
