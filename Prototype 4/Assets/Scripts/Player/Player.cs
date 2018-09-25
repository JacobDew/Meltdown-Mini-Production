using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private GameObject m_Player;

    private GameObject m_pCombo;
    private GameObject m_pHealth;
    private GameObject m_pMultiplier;

    private GameObject m_pCube0;
    private GameObject m_pCube1;
    private GameObject m_pCube2;
    private GameObject m_pCube3;


    private Vector3 m_vForward;
    private Vector3 m_vPlanePoint;
    private Vector3 m_vPlaneNormal;


    private float m_fHealth;

    private float m_fLastShot;

    private int m_iWeapon;
    private float m_fFireDelay;
    private float m_fDamage;

    private int m_iSwapCombo;
    private float m_fComboTimer;


	// Use this for initialization
	void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_pCombo = GameObject.FindGameObjectWithTag("Combo");
        m_pHealth = GameObject.FindGameObjectWithTag("Health");
        m_pMultiplier = GameObject.FindGameObjectWithTag("Multiplier");
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
        m_iSwapCombo = 0;
        m_fComboTimer = 0.0f;
        m_fHealth = 100.0f;
        m_pHealth.GetComponent<Text>().text = "Health: " + m_fHealth.ToString();
        m_pCombo.GetComponent<Text>().text = "Combo: " + m_iSwapCombo.ToString();
        m_pMultiplier.GetComponent<Text>().text = " Multiplier: " + (((float)m_iSwapCombo / 10.0f) + 1.0f).ToString();
    }
	
	// Update is called once per frame
	void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        if (Input.GetMouseButton(0))
		{
			Debug.Log("Pressed left click.");
            if (0.0f > m_fLastShot)
            {
                Debug.Log(m_fLastShot);
                m_fLastShot = m_fFireDelay;
                RaycastHit HitPos;
                if (Physics.Raycast(ray, out HitPos))
                {
                    Debug.Log("Raycast");
                    if (null != HitPos.point)
                    {
                        Debug.Log("Hit!");
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
                        TempObject.GetComponent<ProjectileScript>().SetDirection(Vector3.Normalize(new Vector3(HitPos.point.x - m_Player.transform.position.x,
                            0.0f, HitPos.point.z - m_Player.transform.position.z)), (m_fDamage * ((float)m_iSwapCombo % 10.0f) + 1.0f));

                        //sound effect for bullet
                        FindObjectOfType<AudioManager>().Play("Laser");
                    }
                }
            }
        }
        
		if (Input.GetMouseButton(1)) {
			Debug.Log("Pressed right click.");
		}

		if (Input.GetMouseButton(2))
		{
			Debug.Log("Pressed middle click.");
		}

        m_fLastShot -= Time.deltaTime;
        m_fComboTimer -= Time.deltaTime;
        if (0.0f > m_fComboTimer)
        {
            m_iSwapCombo = -1;
            SetWeapon(m_iWeapon);
        }
        
    }
    
    public void SetWeapon(int _Weapon)
    {
        m_iWeapon = _Weapon;
        m_fComboTimer = 10.0f;
        m_iSwapCombo += 1;
        switch (_Weapon)
        {
            case 0:
                {
                    m_fFireDelay = 0.2f;
                    m_fDamage = 0.6f;
                }
                break;
            case 1:
                {
                    m_fFireDelay = 1.0f;
                    m_fDamage = 5.0f;
                }
                break;
            case 2:
                {
                    m_fFireDelay = 0.1f;
                    m_fDamage = 0.4f;
                }
                break;
            case 3:
                {
                    m_fFireDelay = 0.05f;
                    m_fDamage = 0.15f;
                }
                break;
            default:
                {
                    m_fFireDelay = 0.5f;
                    m_fDamage = 0.15f;
                }
                break;
        }
        m_pCombo.GetComponent<Text>().text = "Combo: " + m_iSwapCombo.ToString();
        m_pMultiplier.GetComponent<Text>().text = " Multiplier: " + (((float)m_iSwapCombo / 10.0f) + +1.0f).ToString();
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
        m_pHealth.GetComponent<Text>().text = "Health: " + m_fHealth.ToString();
    }
}