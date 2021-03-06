﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    AudioManager m_pAudioManager;
    Currency m_pCurrency;
    Text m_pText;
    public GameObject m_pStoreAvailable;

    private GameObject m_pPerkMachine;

    private GameObject m_pHealth;       //  Health display.
    private GameObject m_pMultiShot;
    private GameObject m_pPiercing;
    private GameObject m_pExtraLife;

    private GameObject m_pCube0;        //  Weapon objects.
    private GameObject m_pCube1;
    private GameObject m_pCube2;
    private GameObject m_pCube3;


    private Vector3 m_vPlanePoint;
    private Vector3 m_vPlaneNormal;
    private int LayerMask = 1 << 13;    //  For the ray-cast, unused or not used effectively.

    private int m_iCurrency;            //  Currency value.

    private float m_fHealth = 100.0f;            //  Player Health.
    private int m_iLives;

    private float m_fLastShot;          //  Works with fire-delay.

    public int m_iWeapon;               //  Current Weapon.
    private float m_fFireDelay;         //  Delay between shots.
    private float m_fDamage;            //  Damage of the current weapon.
    public int m_iAmmoCount;            //  Number of shots available.
    private int m_iMultiHit;            //  May be a multi-hit mechanic. Apply extra hits at the cost of pierce?
    private float m_fProjectileSpeed;   //  Speed of the projectiles.
    private int m_iWeaponPierce;        //  Weapon's number of natural pierces.
    private int m_iMultiShot;           //  Number of projectiles.
    private int m_iBasePierce;          //  number of enemies the projectile can hit.
    private int m_iWeaponMultishot;
    private float m_fDelay = 0.0f;

    private float m_fSpread;

	// Use this for initialization
	void Start()
    {
        //  Currency values.
        //m_Player.AddComponent<Currency>();
        m_iCurrency = 0;
        m_iLives = 0;
        m_iWeaponMultishot = 0;
        //  Setting pointers.
        m_pHealth = GameObject.FindGameObjectWithTag("Health");
        m_pAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_pCurrency = this.GetComponent<Currency>();
        m_pText = GameObject.FindGameObjectWithTag("Currency").GetComponent<Text>();

        //  Loading projectiles.
        m_pCube0 = Resources.Load<GameObject>("Cube0");
        m_pCube1 = Resources.Load<GameObject>("Cube1");
        m_pCube2 = Resources.Load<GameObject>("Cube2");
        m_pCube3 = Resources.Load<GameObject>("Cube3");

        m_pMultiShot = GameObject.Find("Multishot");
        m_pPiercing = GameObject.Find("Piercing");
        m_pExtraLife = GameObject.Find("ExtraLife");
        m_pMultiShot.SetActive(false);
        m_pPiercing.SetActive(false);
        m_pExtraLife.SetActive(false);
      
       


        //  Add Plane Point.
        m_vPlanePoint = new Vector3(0.0f, 0.43f, 0.0f);
        m_vPlaneNormal = new Vector3(0.0f, 1.0f, 0.0f);

        //  Weapon presets;
        m_iBasePierce = 0;
        m_fLastShot = 0.0f;
        m_fHealth = 100.0f;

        //  Setting up health display.
        m_pHealth.transform.position = new Vector3(this.transform.position.x , this.transform.position.y , this.transform.position.z );
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().maxValue = 100f;
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().minValue = 0f;

        m_pText.text = "Currency: " + m_iCurrency.ToString();

        if (null == m_pStoreAvailable)
        {
            m_pStoreAvailable = GameObject.Find("Store Text");
        }
        m_pStoreAvailable.SetActive(false);

        //  Starting weapon.
        SetWeapon(0);
    }
	
	// Update is called once per frame
	void Update()
    {
        //  Check input.
        ProcessInput();

        if (m_iLives == 0)
        {
            m_pExtraLife.SetActive(false);
        }

        //Updates posistion and value of Player healthBar
        m_pHealth.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + 5.35f, this.transform.position.z);
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().value = m_fHealth;
        

        m_fLastShot -= Time.deltaTime;
        m_fDelay -= Time.deltaTime;
    }
    
    public void SetWeapon(int _Weapon)
    {
        m_iWeapon = _Weapon;
        switch (_Weapon)
        {
            case 0:
                {
                    m_iAmmoCount = 150;
                    m_fFireDelay = 0.2f;
                    m_fDamage = 2.6f;
                    m_fProjectileSpeed = 20.0f;
                    m_iWeaponPierce = 0;
                    m_iWeaponMultishot = 0;
                    m_fSpread = 0.5f;
                }
                break;
            case 1:
                {
                    m_iAmmoCount = 50;
                    m_fFireDelay = 0.7f;
                    m_fDamage = 13.0f;
                    m_fProjectileSpeed = 50.0f;
                    m_iWeaponPierce = 2;
                    m_iWeaponMultishot = 0;
                    m_fSpread = 0.0f;
                }
                break;
            case 2:
                {
                    m_iAmmoCount = 230;
                    m_fFireDelay = 0.7f;
                    m_fDamage = 1.4f;
                    m_fProjectileSpeed = 90.0f;
                    m_iWeaponPierce = 0;
                    m_iWeaponMultishot = 10;
                    m_fSpread = 0.0f;
                }
                break;
            case 3:
                {
                    m_iAmmoCount = 1000;
                    m_fFireDelay = 0.05f;
                    m_fDamage = 0.85f;
                    m_fProjectileSpeed = 30.0f;
                    m_iWeaponPierce = 0;
                    m_iWeaponMultishot = 0;
                    m_fSpread = 5.0f;
                }
                break;
            default:
                {
                    SetWeapon(0);
                }
                break;
        }

    }

    public void TakeDamage(float _Damage)
    {
        if (0.0f < m_fDelay)
        {
            return;
        }
        m_fDelay = 0.5f;
        m_fHealth -= _Damage;
        if (100.0f < m_fHealth)
        {
            m_fHealth = 100.0f;
        }
        if (0.0001f > m_fHealth)
        {
            m_iLives -= 1;
            if (0 > m_iLives)
            {
                Destroy(GameObject.FindGameObjectWithTag("SpawnControl"));
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                m_fHealth = 100.0f;
            }
        }
        
    }

    public bool AddPerk(int _Type, int _Cost)
    {
        Debug.Log(_Cost + " / " + m_iCurrency);
        if (_Cost > m_iCurrency)
        {
            return false;
        }
        else
        {
            m_iCurrency -= _Cost;
            m_pText.text = "Currency: " + m_iCurrency.ToString();
            switch (_Type)
            {
                //  Purchased text not set.
                //  Consider image swaps once perk is owned.
                case 0:
                    {
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Perk");
                        m_pPiercing.SetActive(true);

                        m_iBasePierce += 2;
                        //m_pPierce.text = "Owned!";
                    }
                    break;
                case 1:
                    {
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Perk");
                        m_pMultiShot.SetActive(true);

                        m_iMultiShot += 6;
                        //m_pMultishot.text = "Owned!";
                    }
                    break;
                case 2:
                    {
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Perk");
                        m_pExtraLife.SetActive(true);

                        m_iLives += 1;
                        //m_pLives.text = "Owned!";
                    }
                    break;
                case 3:
                    {
                        TakeDamage(-50.0f);
                    }
                    break;
                default:
                    {
                        m_pMultiShot.SetActive(false);
                        m_pPiercing.SetActive(false);
                        m_pExtraLife.SetActive(false);
                        m_iBasePierce += 1;
                    }
                    break;
            }
            return true;
        }
        //  Fuck you, C#.
        return false;
    }

    public void AddCurrency(int _Value)
    {
        m_iCurrency += _Value;
        m_pText.text = "Currency: " + m_iCurrency.ToString();
    }
    
    private void ProcessInput()
    {
        //  If left mouse button is pressed and game is not paused.
        if (true == Input.GetMouseButton(0) && 0.0f != Time.timeScale)
        {
            if (0.0f > m_fLastShot && 0 < m_iAmmoCount)
            {
                m_iAmmoCount -= 1;
                RaycastHit HitPos;
                Ray Temp = Camera.main.ScreenPointToRay(Input.mousePosition);
                m_fLastShot = m_fFireDelay;
                if (Physics.Raycast(Temp.origin, Temp.direction, out HitPos, LayerMask))
                {
                    if (null != HitPos.point)
                    {
                        GameObject TempObject;
                        switch (m_iWeapon)
                        {
                            case 0:
                                {
                                    m_pAudioManager.Play("Pistol");
                                    TempObject = Instantiate(m_pCube0);
                                }
                                break;
                            case 1:
                                {
                                    m_pAudioManager.Play("Sniper");
                                    TempObject = Instantiate(m_pCube1);
                                }
                                break;
                            case 2:
                                {
                                    m_pAudioManager.Play("Shotgun");
                                    TempObject = Instantiate(m_pCube2);
                                }
                                break;
                            case 3:
                                {
                                    m_pAudioManager.Play("Machine Gun");
                                    TempObject = Instantiate(m_pCube3);
                                }
                                break;
                            default:
                                {
                                    TempObject = Instantiate(m_pCube0);
                                }
                                break;
                        }
                        Vector3 TempVec = this.transform.position;
                        TempVec.y = TempVec.y + 1.0f;
                        TempObject.transform.position = TempVec;
                        TempObject.transform.rotation = this.transform.rotation;
                        Vector3 FireVector = Quaternion.Euler(0, Random.Range(-m_fSpread, m_fSpread), 0) *
                                Vector3.Normalize(new Vector3(HitPos.point.x - this.transform.position.x, 0.0f, HitPos.point.z - this.transform.position.z));
                        TempObject.GetComponent<ProjectileScript>().Initialize(m_iWeapon, FireVector, m_fDamage, m_fProjectileSpeed, m_iBasePierce + m_iWeaponPierce, m_iMultiShot + m_iWeaponMultishot);

                    }
                }
            }
        }
        //  Checking if middle mouse button is pressed.
        if (true == Input.GetMouseButton(1))
        {

        }
        //  Checking if right mouse button is pressed.
        if (true == Input.GetMouseButton(2))
        {

        }

        if (null != m_pPerkMachine)
        {
            if (null == m_pStoreAvailable)
            {
                m_pStoreAvailable = GameObject.Find("Store Text");
            }
            if (7.0f > Vector3.Distance(m_pPerkMachine.transform.position, this.transform.position))
            {
                //  Display hotkey to open perk store.
                m_pStoreAvailable.SetActive(true);
            }
            else
            {
                m_pStoreAvailable.SetActive(false);
            }
            if (true == Input.GetKeyDown(KeyCode.F))
            {
                if (0.0f == Time.timeScale)
                {
                    //  Closable from any position for safety purposes.
                    //  Close perk menu.
                    GetComponent<Currency>().Return();
                }
                else if (7.1f > Vector3.Distance(m_pPerkMachine.transform.position, this.transform.position))
                {
                    //  Only opened if in range.
                    //  Open perk menu.
                    GetComponent<Currency>().OpenPerkMenu();
                }
            }
        }
        else
        {
            m_pPerkMachine = GameObject.FindGameObjectWithTag("PerkMachine");
        }
    }

}