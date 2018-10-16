using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public  bool GameIsPaused = false;
    public  bool FirstFrame = false;

    private GameObject m_Player;

    public GameObject PauseMenuUI;

    private GameObject m_ShotgunSprite;
    private GameObject m_SniperSprite;
    private GameObject m_PistolSprite;
    private GameObject m_MachineGunSprite;

    private GameObject m_AmnoCounter;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_ShotgunSprite = GameObject.Find("Shotgun");
        m_SniperSprite = GameObject.Find("Sniper");
        m_PistolSprite = GameObject.Find("Pistol");
        m_MachineGunSprite = GameObject.Find("Machine Gun");
        m_AmnoCounter = GameObject.Find("Current Ammo");

        HUDCheck();

        Time.timeScale = 1;
        GameIsPaused = false;
        FirstFrame = false;
        PauseMenuUI.SetActive(true);
        
    }
	
	// Update is called once per frame
	void Update () {

        HUDCheck();

        if (FirstFrame == false)
        {
            PauseMenuUI.SetActive(false);
            FirstFrame = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused)
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Select");
                Resume();
            }
            else
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Pause");
                Pause();
            }
        }

	}

    public void Resume()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Select");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;

        GameIsPaused = false;
    }

    void Pause()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Pause");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;

        GameIsPaused = true;
    }

    public void MainMenu()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu Select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void HUDCheck()
    {
        switch (m_Player.GetComponent<Player>().m_iWeapon)
        {
            //Pistol
            case 0:
                {
                    m_PistolSprite.SetActive(true);
                    m_ShotgunSprite.SetActive(false);
                    m_SniperSprite.SetActive(false);
                    m_MachineGunSprite.SetActive(false);
                }
                break;
                //Sniper
            case 1:
                {
                    m_PistolSprite.SetActive(false);
                    m_ShotgunSprite.SetActive(false);
                    m_SniperSprite.SetActive(true);
                    m_MachineGunSprite.SetActive(false);
                }
                break;
                //shotgun
            case 2:
                {
                    m_PistolSprite.SetActive(false);
                    m_ShotgunSprite.SetActive(true);
                    m_SniperSprite.SetActive(false);
                    m_MachineGunSprite.SetActive(false);
                }
                break;
                //mechine gun
            case 3:
                {
                    m_PistolSprite.SetActive(false);
                    m_ShotgunSprite.SetActive(false);
                    m_SniperSprite.SetActive(false);
                    m_MachineGunSprite.SetActive(true);
                }
                break;
            default:
                {
                    
                }
                break;
        }


        m_AmnoCounter.GetComponent<Text>().text =  m_Player.GetComponent<Player>().m_iAmmoCount.ToString();


    }

}
