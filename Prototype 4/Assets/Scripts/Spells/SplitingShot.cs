using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitingShot : MonoBehaviour
{
    private GameObject m_pEffectCubeX;

    Vector3 m_vForward = new Vector3(-0.7071f, 0.0f, -0.7071f);

    private float m_fProjectileTimer = 0.0f;
    private float m_fProjectileDelay = 0.75f;
    private float m_fSpeed = 8.0f;

    private float m_fDelta = 0.0f;

    // Use this for initialization
    void Start()
    {
        this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 45.0f);
        m_pEffectCubeX = Resources.Load<GameObject>("EffectCubeY");
	}
	
	// Update is called once per frame
	void Update()
    {
        m_fDelta = Time.deltaTime;
        this.transform.position += m_vForward * m_fSpeed * m_fDelta;
        if (m_fProjectileDelay < m_fProjectileTimer)
        {
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(1.0f, 0.0f, 0.0f));
            Instantiate(m_pEffectCubeX, this.transform.position, this.transform.rotation).AddComponent<Shot>().Initialize(new Vector3(-1.0f, 0.0f, 0.0f));
            m_fProjectileTimer = 0.0f;
        }
        m_fProjectileTimer += m_fDelta;
    }

    public void SetDirection(Vector3 _Direction)
    {
        m_vForward = _Direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
