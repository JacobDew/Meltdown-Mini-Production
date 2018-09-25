using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon0 : MonoBehaviour
{
    private GameObject m_pPlayer;
    public int m_iWeaponType = 0;


	// Use this for initialization
	void Start()
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update()
    {
		if (2.0f > Vector3.Distance(this.transform.position, m_pPlayer.transform.position))
        {
            m_pPlayer.GetComponent<Player>().SetWeapon(m_iWeaponType);
            Destroy(this.gameObject);
        }
	}

    public void Initialize(int _Weapon)
    {
        m_iWeaponType = _Weapon;
    }
}
