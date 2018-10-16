using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private GameObject m_pPlayer;

    private GameObject[] m_pAttacks;

    private string[] m_sFunctions;

    //Create a timer to script everything nicely


	// Use this for initialization
	void Start()
    {
        Invoke(m_sFunctions[Random.Range(0, m_sFunctions.Length)], 0.0f); //use this to enable attacks
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

    private void LoadAssets()
    {
        m_pAttacks = new GameObject[] { Resources.Load<GameObject>("EffectCubeZ") };
    }

    void Attack1()
    {
        GameObject TempPtr = Instantiate(m_pAttacks[0]);
        TempPtr.AddComponent<SpiralShot>().SetDirection(GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position);


    }

    void Attack2()
    {



    }
    //etc
}
