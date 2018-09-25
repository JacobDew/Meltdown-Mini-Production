using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	
    private GameObject m_Player;
    private GameObject[] m_Followers;

    private float m_fDistance;
    private Vector3 m_vTarget;
    private Vector3 m_vForward;
    private System.Random m_rRandom;
    private float m_fDelay;
    private bool m_bArraySet;

    private float m_fSpeed;

    private const float c_fMaxSpeed = 4.5f;
    private const float c_fForce = 0.15f;

    private float m_fHealth;

    private float m_fDamage;
    private float m_fDamageDelay;


    // Use this for initialization
    void Start()
    {
        m_fSpeed = 0.0f;
        m_bArraySet = false;
        m_fDelay = 0.0f;
        m_vForward = new Vector3(1.0f, 0.0f, 0.0f);
        m_Player = GameObject.FindGameObjectsWithTag("Player")[0];
        m_rRandom = new System.Random();
        m_fDamage = 5.0f;
        m_fDamageDelay = 0.0f;
        m_fHealth = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f > m_fDamageDelay)
        {
            if (1.5f > Vector3.Distance(this.transform.position, m_Player.transform.position))
            {
                m_Player.GetComponent<Player>().TakeDamage(m_fDamage);
                m_fDamageDelay = 1.0f;
            }
        }
        m_vTarget = m_Player.transform.position - this.transform.position;
        m_fDistance = Vector3.Magnitude(m_vTarget);
        if (20.0f > m_fDistance)
        {
            m_fSpeed = c_fMaxSpeed;

            if (0.5f > m_fDistance)
            {
                m_Player.GetComponent<Player>().TakeDamage(5.0f);
            }
        }
        else
        {
            m_fSpeed = c_fMaxSpeed;
        }
        m_vForward += ((Vector3.Normalize(m_vTarget - m_vForward) * c_fMaxSpeed) - m_vForward) * c_fForce;
        Vector3 Temp = ((m_vForward * m_fSpeed) * Time.deltaTime);
        this.transform.Translate(Temp.x, 0.0f, Temp.z);

        Quaternion targetRotation = Quaternion.LookRotation(m_Player.transform.position - transform.position);

        //transform.rotation = targetRotation;
        m_vTarget -= Temp;


            m_fDelay += Time.deltaTime;
         
                m_Followers = GameObject.FindGameObjectsWithTag("Follower");
                m_bArraySet = true;
        m_fDamageDelay -= Time.deltaTime;

    }

    //Collision with bullet
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            m_fHealth -= other.gameObject.GetComponent<ProjectileScript>().GetDamage();
            Destroy(other.gameObject);
            if (0.0f > m_fHealth)
            {
                GameObject Temp = Resources.Load<GameObject>("X");
                Vector3 vTemp = this.transform.position;
                short sX = 0;
                while (6 > sX)
                {
                    GameObject Temp2 = Instantiate(Temp);
                    Temp2.transform.position = this.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), -1.0f, Random.Range(-0.3f, 0.3f));
                    Temp2.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                    sX++;
                }
                Destroy(gameObject);
            }
        }

    }

    void Steer(Vector3 _Direction, float _Force)
    {
	    m_vForward += ((Vector3.Normalize(_Direction - m_vForward) * c_fMaxSpeed) - m_vForward) * _Force;
    }
}
