using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{

    private float m_fHealth = 2.0f;

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void TakeDamage(float _Damage)
    {
        m_fHealth -= _Damage;
        if (0.0001f > m_fHealth)
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

    public void Initialize(float _Health)
    {
        m_fHealth = _Health;
    }
}
