using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    private GameObject m_pPlayer;

    private float m_fHealth = 2.0f;

    private bool m_bDead = false;

    // Use this for initialization
    void Start()
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void TakeDamage(float _Damage)
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Enemy Impact");
        m_fHealth -= _Damage;
        m_pPlayer.GetComponent<Player>().AddCurrency(5);
        if (0.0001f > m_fHealth)
        {
            GameObject Temp = Resources.Load<GameObject>("EffectCube");
            Vector3 vTemp = this.transform.position;
            short sX = 0;
            while (15 > sX)
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Enemy Death");
                GameObject Temp2 = Instantiate(Temp);
                Temp2.AddComponent<CubeScript>().Initialize(Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
                Temp2.transform.localScale *= 2.0f;
                Temp2.transform.position = this.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), 0.4f, Random.Range(-1.5f, 1.5f));
                Temp2.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                sX++;
            }
            if (false == m_bDead)
            {
                m_pPlayer.GetComponent<Player>().AddCurrency(55);
                m_bDead = true;
            }
            Destroy(gameObject);
        }
    }

    public void Initialize(float _Health)
    {
        m_fHealth = _Health;
    }
}
