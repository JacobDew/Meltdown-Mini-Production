using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour {

	
    private GameObject m_Player;
    private GameObject[] m_Followers;

    private NavMeshAgent Agent;

    private Vector3 m_vTarget;

    private float m_fSpeed;
    private const float c_fMaxSpeed = 4.5f;
    private float m_fDamage;
    private float m_fDamageDelay;


    // Use this for initialization
    void Start()
    {
        if ("Level 1" != SceneManager.GetActiveScene().name && "Level 2" != SceneManager.GetActiveScene().name)
        {
            Destroy(this.gameObject);
        }
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = c_fMaxSpeed * 5;

        m_Player = GameObject.FindGameObjectWithTag("Player");

        m_fDamage = 5.0f;
        m_fDamageDelay = 0.0f;

        this.gameObject.AddComponent<EnemyHealthScript>();
        this.gameObject.GetComponent<EnemyHealthScript>().Initialize(2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (null != m_Player && null != this.gameObject)
        {
            if (0.0f > m_fDamageDelay)
            {
                if (1.5f > Vector3.Distance(this.transform.position, m_Player.transform.position))
                {
                    m_Player.GetComponent<Player>().TakeDamage(m_fDamage);
                    m_fDamageDelay = 1.0f;
                }
            }
            m_vTarget = m_Player.transform.position;
            Agent.SetDestination(m_vTarget);
 

            m_fDamageDelay -= Time.deltaTime;
        }
        else
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    
}
