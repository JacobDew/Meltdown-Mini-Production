using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMain : MonoBehaviour
{
    private GameObject[] m_pWeaponObject;

    private float m_fTimer;


	// Use this for initialization
	void Start()
    {
        m_pWeaponObject = new GameObject[] { Resources.Load<GameObject>("Pistol"), Resources.Load<GameObject>("Sniper"), Resources.Load<GameObject>("Shotgun"),
            Resources.Load<GameObject>("AssaultRifle") };
    }

    // Update is called once per frame
    void Update()
    {
        if (5.0f < m_fTimer)
        {
            int WeaponType = Random.Range(0, 4);
            GameObject Temp = Instantiate(m_pWeaponObject[WeaponType]);
            Temp.transform.position = new Vector3(Random.Range(-30.0f, 30.0f), 1.0f, Random.Range(-50.0f, 50.0f));
            Temp.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            Temp.AddComponent<Weapon0>();
            Temp.GetComponent<Weapon0>().Initialize(WeaponType);
            //Temp.GetComponent<Renderer>().material = Resources.Load<Material>(WeaponType.ToString());
            if ("Level 1" == SceneManager.GetActiveScene().name)
            {
                m_fTimer = 0.0f;
            }
            else
            {
                m_fTimer = -7.0f;
            }
        }
        m_fTimer += Time.deltaTime;
	}
}
