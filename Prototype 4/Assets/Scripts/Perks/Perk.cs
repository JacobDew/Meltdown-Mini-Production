﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{
    private GameObject m_pPlayer;
    private Vector3 m_fHoverVec;
    private Vector3 m_vRotationAxis = new Vector3(0.0f, 1.0f, 0.0f);

    private int m_iPerk = 0;

    private float m_fHoverDirection;
    private float m_fRotation;
    private float m_fHoverSpeed;

	// Use this for initialization
	void Start()
    {
        m_fRotation = Random.Range(-1.0f, 1.0f) * 100.0f;
        m_fHoverVec = new Vector3(0.0f, 0.0f, 0.0f);
        m_fHoverDirection = Random.Range(1.0f, 50.0f);
        m_fHoverVec.y = m_fHoverDirection;
        m_fHoverSpeed = 0.04f;
    }
	
	// Update is called once per frame
	void Update()
    {
        this.transform.Rotate(m_vRotationAxis, m_fRotation * Time.deltaTime);
        if (0.3f > this.transform.position.y)
        {
            m_fHoverVec.y = m_fHoverDirection;
        }
        else if (1.0f < this.transform.position.y)
        {
            m_fHoverVec.y = -m_fHoverDirection;
        }
        this.transform.Translate(m_fHoverVec * m_fHoverSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_pPlayer.GetComponent<Player>().AddPerk(m_iPerk, 0);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 _Position, int _Type)
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
        m_iPerk = _Type;
        this.transform.position = _Position;
        m_fRotation = Random.Range(-1.0f, 1.0f) * 100.0f;
        m_fHoverVec = new Vector3(0.0f, 0.0f, 0.0f);
        m_fHoverDirection = Random.Range(1.0f, 50.0f);
        m_fHoverVec.y = m_fHoverDirection;
        m_fHoverSpeed = 0.04f;

        //  Load different models depending on perk type.
        switch (m_iPerk)
        {
            case 0:
                {

                }
                break;
            case 1:
                {

                }
                break;
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;
            case 4:
                {

                }
                break;
            default:
                {

                }
                break;
        }
    }

}
