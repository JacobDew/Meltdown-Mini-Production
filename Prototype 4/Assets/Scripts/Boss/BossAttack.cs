using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject m_pPlayer;

    GameObject m_pCubeZ;

    Vector3 m_vBossPosition;

    //Create a timer to script everything nicely
    string[] m_sFunctions;
    
    float m_fTimer = 0.0f;


	// Use this for initialization
	void Start()
    {
        m_sFunctions = new string[] { "Attack1", "Attack2", "Attack3" };
        m_pCubeZ = Resources.Load<GameObject>("EffectCubeZ");
        m_pCubeZ.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Y");
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update()
    {
		if (0.0001f > m_fTimer)
        {
            if (3 > Random.Range(0, 8))
            {
                Invoke("Attack3", 0.0f);
                Invoke("Attack3", 2.0f);
                Invoke("Attack3", 3.0f);
                Invoke("Attack3", 5.0f);
            }
            Invoke(m_sFunctions[Random.Range(0, m_sFunctions.Length)], 0.0f);
        }
        m_fTimer -= Time.deltaTime;
	}



    void Attack1()
    {
        GameObject TempPtr = Instantiate(m_pCubeZ);
        TempPtr.transform.position = this.transform.position;
        TempPtr.AddComponent<SpiralShot>().SetDirection(Vector3.Normalize(new Vector3(m_pPlayer.transform.position.x - this.transform.position.x, 0.0f,
            m_pPlayer.transform.position.z - this.transform.position.z)));
        m_fTimer = 5.0f;
    }

    void Attack2()
    {
        GameObject TempPtr = Instantiate(m_pCubeZ);
        TempPtr.transform.position = this.transform.position;
        TempPtr.AddComponent<SplitingShot>();
        m_fTimer = 8.0f;
    }

    void Attack3()
    {
        GameObject TempPtr = Instantiate(Resources.Load<GameObject>("Pillar"));
        TempPtr.transform.position = new Vector3(Random.Range(-25.0f, 44.0f), 5.0f, Random.Range(-27.0f, 37.0f));
        m_fTimer = 3.0f;
    }

}
