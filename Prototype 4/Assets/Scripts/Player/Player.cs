using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private GameObject m_Player;

    private GameObject m_pHealth;

    private GameObject m_pCube0;
    private GameObject m_pCube1;
    private GameObject m_pCube2;
    private GameObject m_pCube3;


    private Vector3 m_vForward;
    private Vector3 m_vPlanePoint;
    private Vector3 m_vPlaneNormal;
    private int LayerMask = 1 << 13;


    private float m_fHealth;

    private float m_fLastShot;

    private int m_iWeapon;
    private float m_fFireDelay;
    private float m_fDamage;
    private int m_iAmmoCount;
    private int m_iHits;
    private float m_fProjectileSpeed;
    private int m_iBasePierce;  //  number of enemies the projectile can hit.

    private int m_iSwapCombo;
    private float m_SwapMultiplier;
    private float m_fComboTimer;


	// Use this for initialization
	void Start()
    {
        m_fProjectileSpeed = 20;
        m_iBasePierce = 1;
        m_iAmmoCount = 200;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_pHealth = GameObject.FindGameObjectWithTag("Health");
        m_pCube0 = Resources.Load<GameObject>("Cube0");
        m_pCube1 = Resources.Load<GameObject>("Cube1");
        m_pCube2 = Resources.Load<GameObject>("Cube2");
        m_pCube3 = Resources.Load<GameObject>("Cube3");
        //  Add Plane Point.
        m_vPlaneNormal = new Vector3(0.0f, 1.0f, 0.0f);
        m_vForward = new Vector3(0.0f, 0.0f, 1.0f);
        m_fLastShot = 0.0f;
        m_fFireDelay = 0.2f;
        m_iWeapon = 0;
        m_fDamage = 2.6f;
        m_iSwapCombo = 0;
        m_fComboTimer = 0.0f;
        m_fHealth = 100.0f;
        m_pHealth.transform.position = new Vector3(m_Player.transform.position.x , m_Player.transform.position.y , m_Player.transform.position.z );
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().maxValue = 100f;
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().minValue = 0f;
    }
	
	// Update is called once per frame
	void Update()
    {
        //Updates posistion and value of Player healthBar
        m_pHealth.transform.position = new Vector3(m_Player.transform.position.x , m_Player.transform.position.y + 2.0f, m_Player.transform.position.z);
        m_pHealth.transform.Find("Panel/Slider").gameObject.GetComponent<Slider>().value = m_fHealth;
        if (Input.GetMouseButton(0))
		{
            if (0.0f > m_fLastShot && 0 < m_iAmmoCount)
            {
                m_iAmmoCount -= 1;
                RaycastHit HitPos;
                Ray Temp = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log(m_iAmmoCount);
                m_fLastShot = m_fFireDelay;
                if (Physics.Raycast(Temp.origin, Temp.direction, out HitPos, LayerMask))
                {
                    if (null != HitPos.point)
                    {
                        Debug.Log(HitPos.point);
                        GameObject TempObject;
                        switch (m_iWeapon)
                        {
                            case 0:
                                {
                                    TempObject = Instantiate(m_pCube0);
                                }
                                break;
                            case 1:
                                {
                                    TempObject = Instantiate(m_pCube1);
                                }
                                break;
                            case 2:
                                {
                                    TempObject = Instantiate(m_pCube2);
                                }
                                break;
                            case 3:
                                {
                                    TempObject = Instantiate(m_pCube3);
                                }
                                break;
                            default:
                                {
                                     TempObject = Instantiate(m_pCube0);
                                }
                                break;
                        }
                        TempObject.transform.position = m_Player.transform.position;
                        TempObject.transform.rotation = m_Player.transform.rotation;
                        TempObject.GetComponent<ProjectileScript>().Initialize(Vector3.Normalize(new Vector3(HitPos.point.x - m_Player.transform.position.x,
                            0.0f, HitPos.point.z - m_Player.transform.position.z)), (m_fDamage * m_SwapMultiplier), m_fProjectileSpeed, m_iBasePierce);

                        //sound effect for bullet
                        FindObjectOfType<AudioManager>().Play("Laser");
                    }
                }
            }
        }
        
		if (Input.GetMouseButton(1))
        {

		}

		if (Input.GetMouseButton(2))
		{

		}

        m_fLastShot -= Time.deltaTime;
        m_fComboTimer -= Time.deltaTime;
        if (0.0f > m_fComboTimer)
        {
            m_iSwapCombo = 0;
            m_SwapMultiplier = ((float)m_iSwapCombo / 10.0f) + 1.0f;
            //SetWeapon(m_iWeapon);
        }
        
    }
    
    public void SetWeapon(int _Weapon)
    {
        m_iWeapon = _Weapon;
        m_fComboTimer = 10.0f;
        m_iSwapCombo += 1;
        m_SwapMultiplier = ((float)m_iSwapCombo / 10.0f) + 1.0f;
        switch (_Weapon)
        {
            case 0:
                {
                    m_iAmmoCount = 150;
                    m_fFireDelay = 0.2f;
                    m_fDamage = 2.6f;
                    m_fProjectileSpeed = 20.0f;
                }
                break;
            case 1:
                {
                    m_iAmmoCount = 50;
                    m_fFireDelay = 1.0f;
                    m_fDamage = 20.0f;
                    m_fProjectileSpeed = 50.0f;
                }
                break;
            case 2:
                {
                    m_iAmmoCount = 230;
                    m_fFireDelay = 0.1f;
                    m_fDamage = 1.4f;
                    m_fProjectileSpeed = 90.0f;
                }
                break;
            case 3:
                {
                    m_iAmmoCount = 1000;
                    m_fFireDelay = 0.05f;
                    m_fDamage = 0.85f;
                    m_fProjectileSpeed = 30.0f;
                }
                break;
            default:
                {
                    m_iAmmoCount = 150;
                    m_fFireDelay = 0.2f;
                    m_fDamage = 2.6f;
                    m_fProjectileSpeed = 20.0f;
                }
                break;
        }

    }

    public void TakeDamage(float _Damage)
    {
        m_fHealth -= _Damage;
        if (100.0f < m_fHealth)
        {
            m_fHealth = 100.0f;
        }
        if (0.01f > m_fHealth)
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }

    public void AddPerk(int _Type)
    {
        switch (_Type)
        {
            case 0:
                {
                    m_iBasePierce += 1;
                }
                break;
            case 1:
                {

                }
                break;
            case 2:
                {

                }
                break;
            default:
                {
                    m_iBasePierce += 1;
                }
                break;
        }
    }
}