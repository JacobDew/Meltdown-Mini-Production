using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerEnemeyAI : MonoBehaviour
{
    
    private GameObject m_Player;
    private GameObject m_pTower;

    private NavMeshAgent Agent;

    private Vector3 m_vTarget;

    private const float c_fMaxSpeed = 4.5f;
    private float m_fDamage;
    private float m_fDamageDelay;


    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = c_fMaxSpeed * 5;

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_pTower = GameObject.FindGameObjectWithTag("Tower");

        m_fDamage = 7.0f;
        m_fDamageDelay = 0.0f;

        this.gameObject.AddComponent<EnemyHealthScript>();
        this.gameObject.GetComponent<EnemyHealthScript>().Initialize(3.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (null != m_Player && null != m_pTower && null != m_pTower)
        {
            if (0.0f > m_fDamageDelay)
            {
                //Player Damage
                if (1.5f > Vector3.Distance(this.transform.position, m_Player.transform.position))
                {
                    m_Player.GetComponent<Player>().TakeDamage(m_fDamage);
                    m_fDamageDelay = 1.0f;
                }
                //Tower Damage
                if (10f > Vector3.Distance(this.transform.position, new Vector3(m_pTower.transform.position.x, m_pTower.transform.position.y, m_pTower.transform.position.z + 10.0f)))
                {
                    m_pTower.GetComponent<TowerHealth>().TakeDamage(m_fDamage/4.0f);
                    m_fDamageDelay = 1.0f;
                }
            }



            m_vTarget = new Vector3( m_pTower.transform.position.x, m_pTower.transform.position.y, m_pTower.transform.position.z + 12.5f ) ;
            Agent.destination = m_vTarget;


            m_fDamageDelay -= Time.deltaTime;
        }
        else
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_pTower = GameObject.FindGameObjectWithTag("Tower");
        }
    }
}
