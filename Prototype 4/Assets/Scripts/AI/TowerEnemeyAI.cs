using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerEnemeyAI : MonoBehaviour {

    private GameObject m_Target;
    private GameObject[] m_Followers;
    private GameObject m_Player;
    private GameObject m_pTower;

    private NavMeshAgent Agent;

    private float m_fDistance;
    private Vector3 m_vTarget;
    private Vector3 m_vForward;
    private System.Random m_rRandom;
    private float m_fDelay;
    private bool m_bArraySet;

    private float m_fSpeed;

    private const float c_fMaxSpeed = 4.5f;
    private const float c_fForce = 0.15f;


    private float m_fDamage;
    private float m_fDamageDelay;
    

    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = c_fMaxSpeed * 5;

        m_fSpeed = 0.0f;
        m_bArraySet = false;
        m_fDelay = 0.0f;
        m_vForward = new Vector3(1.0f, 0.0f, 0.0f);
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Target = GameObject.FindGameObjectWithTag("Tower");
        m_rRandom = new System.Random();
        m_fDamage = 7.0f;
        m_fDamageDelay = 0.0f;
        m_pTower = GameObject.FindGameObjectWithTag("Tower");
        this.gameObject.AddComponent<EnemyHealthScript>();
        this.gameObject.GetComponent<EnemyHealthScript>().Initialize(3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f > m_fDamageDelay)
        {
            if (1.5f > Vector3.Distance(this.transform.position, m_pTower.transform.position))
            {
                m_pTower.GetComponent<TowerHealth>().TakeDamage(m_fDamage / 4.0f);
                m_fDamageDelay = 1.0f;
            }
            else if (1.5f > Vector3.Distance(this.transform.position, m_Player.transform.position))
            {
                m_Player.GetComponent<Player>().TakeDamage(m_fDamage);
                m_fDamageDelay = 1.0f;
            }
        }
        m_vTarget = m_Target.transform.position - this.transform.position;
        m_fDistance = Vector3.Magnitude(m_vTarget);
        if (20.0f > m_fDistance)
        {
            m_fSpeed = c_fMaxSpeed;
        }
        else
        {
            m_fSpeed = c_fMaxSpeed;
        }
        m_vForward += ((Vector3.Normalize(m_vTarget - m_vForward) * c_fMaxSpeed) - m_vForward) * c_fForce;
        Vector3 Temp = ((m_vForward * m_fSpeed) * Time.deltaTime);
        Agent.destination = m_vTarget;

        Quaternion targetRotation = Quaternion.LookRotation(m_Target.transform.position - transform.position);

       // transform.rotation = targetRotation;
        m_vTarget -= Temp;

        if (gameObject.transform.position != m_vTarget)
        {
            Agent.SetDestination(m_vTarget);
        }
        else
        {
            Agent.isStopped = true;
        }


        m_fDelay += Time.deltaTime;

        m_Followers = GameObject.FindGameObjectsWithTag("Follower");
        m_bArraySet = true;
        m_fDamageDelay -= Time.deltaTime;

    }

    void Steer(Vector3 _Direction, float _Force)
    {
        m_vForward += ((Vector3.Normalize(_Direction - m_vForward) * c_fMaxSpeed) - m_vForward) * _Force;
    }
}
