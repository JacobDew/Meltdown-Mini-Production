using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMain : MonoBehaviour
{
    private GameObject m_pPlayer;

    private GameObject m_pWeaponObject;

    private float m_fTimer;


	// Use this for initialization
	void Start()
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
        m_pWeaponObject = Resources.Load<GameObject>("Capsule");

    }

    // Update is called once per frame
    void Update()
    {
        if (5.0f < m_fTimer)
        {
            GameObject Temp = Instantiate(m_pWeaponObject);
            Temp.transform.position = new Vector3(Random.Range(-20.0f, 20.0f), 1.0f, Random.Range(-20.0f, 20.0f));
            Temp.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            Temp.GetComponent<Weapon0>().Initialize(Random.Range(0, 3));
            m_fTimer = 0.0f;
        }
        m_fTimer += Time.deltaTime;
	}
}
