using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency : MonoBehaviour
{
    private GameObject m_pPerkMenu;
    private GameObject m_pPlayer;

    private int[] m_iPrice;


	// Use this for initialization
	void Start()
    {
        m_pPlayer = GameObject.FindGameObjectWithTag("Player");
        m_pPerkMenu = GameObject.FindGameObjectWithTag("PerkMenu");
        m_pPerkMenu.SetActive(false);
        LoadWares();
	}

    private void LoadWares()
    {
        m_iPrice = new int[] { 1, 1, 1 };
    }

    public void OpenPerkMenu()
    {
        m_pPerkMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PerkOne()
    {
        m_pPlayer.GetComponent<Player>().AddPerk(0, m_iPrice[0]);
    }

    public void PerkTwo()
    {
        m_pPlayer.GetComponent<Player>().AddPerk(1, m_iPrice[1]);
    }

    public void PerkThree()
    {
        m_pPlayer.GetComponent<Player>().AddPerk(2, m_iPrice[2]);
    }

    public void Return()
    {
        m_pPerkMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
