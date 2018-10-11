using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency : MonoBehaviour
{
    private GameObject m_pStore;

    private float m_fRadius = 0.0f;

    private GameObject[] m_pWares;

    private int[] m_iPrice;


	// Use this for initialization
	void Start()
    {
        LoadWares();
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public bool InStore(Vector3 _Position)
    {
        if (null != m_pStore)
        {
            if ( m_fRadius > Vector3.Distance(m_pStore.transform.position, _Position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return false;
    }

    public bool Purchase(int _Type, int _Currency)
    {
        if (-1 < _Type && m_pWares.Length > _Type && m_iPrice.Length > _Type)
        {
            if (m_iPrice[_Type] < _Currency)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return false;
    }

    private void LoadWares()
    {
        m_pWares = new GameObject[] { Resources.Load<GameObject>("Perk") };
        m_iPrice = new int[] { };
    }
}
