using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private GameObject m_pBossHealth;

    private GameObject[] m_pProjectiles;

    private Vector3 Hitbox1;
    private Vector3 Hitbox2;
    private Vector3 Hitbox3;

    private float m_fHealth = 1000.0f;
    private const float m_fMaxHealth = 1000.0f;


	// Use this for initialization
	void Start()
    {
        m_pBossHealth = GameObject.Find("BossHealthBar/Panel/Slider");
        m_pBossHealth.GetComponent<Slider>().value = m_fHealth / m_fMaxHealth;
        Hitbox1 = new Vector3(20.0f, 1.3f, 27.1f);
        Hitbox2 = new Vector3(35.8f, 1.3f, 15.3f);
        Hitbox3 = new Vector3(34.5f, 1.3f, 28.5f);
    }
	
	// Update is called once per frame
	void Update()
    {
        m_pProjectiles = GameObject.FindGameObjectsWithTag("Bullet");
        if (0 < m_pProjectiles.Length)
        {
            for (int iX = 0; iX < m_pProjectiles.Length; iX++)
            {
                if (3.0f > Vector3.Distance(m_pProjectiles[iX].transform.position, Hitbox1))
                {
                    TakeDamage(m_pProjectiles[iX].GetComponent<ProjectileScript>().GetDamage());
                    m_pProjectiles[iX].GetComponent<ProjectileScript>().DestroyEffect();
                }
                else if (4.0f > Vector3.Distance(m_pProjectiles[iX].transform.position, Hitbox2))
                {
                    TakeDamage(m_pProjectiles[iX].GetComponent<ProjectileScript>().GetDamage());
                    m_pProjectiles[iX].GetComponent<ProjectileScript>().DestroyEffect();
                }
                else if (9.0f > Vector3.Distance(m_pProjectiles[iX].transform.position, Hitbox3))
                {
                    TakeDamage(m_pProjectiles[iX].GetComponent<ProjectileScript>().GetDamage());
                    m_pProjectiles[iX].GetComponent<ProjectileScript>().DestroyEffect();
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boss Trigger On Projectile");
        if (other.tag == "Bullet")
        {
            TakeDamage(7.0f);
        }
    }

    public void TakeDamage(float _Damage)
    {
        Debug.Log("DamageTake Boss");
        m_fHealth -= _Damage;
        if (0.0f > m_fHealth)
        {
            m_fHealth = 0.0f;
            //  Scene transition delay.
            //  Go to credits.
            GameObject.Find("Fade Canvas").GetComponent<LevelChanger>().FadeToLevel("Credits");
        }
        m_pBossHealth.GetComponent<Slider>().value = m_fHealth / m_fMaxHealth;
    }
}
