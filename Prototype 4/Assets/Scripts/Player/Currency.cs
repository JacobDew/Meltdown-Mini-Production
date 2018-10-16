using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency : MonoBehaviour
{
    private GameObject m_pPerkMenu;
    private GameObject m_pPlayer;

    private int[] m_iPrice;
    private bool[] m_bPurchased;


	// Use this for initialization
	void Start()
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
        m_pPerkMenu = GameObject.FindGameObjectWithTag("PerkMenu");
        m_pPerkMenu.SetActive(false);
        m_bPurchased = new bool[] { false, false, false };
        LoadWares();
	}

    private void LoadWares()
    {
        m_iPrice = new int[] { 2000, 3000, 5000 };
    }

    public void OpenPerkMenu()
    {
        Debug.Log("Pause");
        m_pPerkMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PerkOne()
    {
        if (false == m_bPurchased[0])
        {
            Debug.Log("Perk1");
            if (true == m_pPlayer.GetComponent<Player>().AddPerk(0, m_iPrice[0]))
            {
                m_bPurchased[0] = true;
            }
        }
    }

    public void PerkTwo()
    {
        if (false == m_bPurchased[1])
        {
            Debug.Log("Perk2");
            if (true == m_pPlayer.GetComponent<Player>().AddPerk(1, m_iPrice[1]))
            {
                m_bPurchased[1] = true;
            }
        }
    }

    public void PerkThree()
    {
        if (false == m_bPurchased[2])
        {
            Debug.Log("Perk3");
            if (true == m_pPlayer.GetComponent<Player>().AddPerk(2, m_iPrice[2]))
            {
                m_bPurchased[2] = true;
            }
        }
    }

    public void Return()
    {
        Debug.Log("Resume");
        m_pPerkMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
