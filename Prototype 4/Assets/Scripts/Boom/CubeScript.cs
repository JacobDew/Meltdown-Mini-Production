using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Vector3 m_vDirection;
    private float m_fSpeed;
    private float m_fDecay;


    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		if (0.01f < m_fSpeed)
        {
            this.transform.Translate(m_vDirection * m_fSpeed * Time.deltaTime);
            this.transform.localScale *= 1.0f - Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        m_fSpeed *= m_fDecay * 1.0f + Time.deltaTime;
	}

    public void Initialize(Quaternion _Rotation, int _Material, float _Speed = 20.0f, float _Decay = 0.5f)
    {
        m_vDirection = new Vector3(0.0f, 1.0f, 0.0f);
        this.transform.rotation = _Rotation;
        m_fSpeed = _Speed;
        m_fDecay = _Decay;
        switch(_Material)
        {
            case 0:
                {
                    //  Green.
                    this.GetComponent<Renderer>().material = Resources.Load<Material>("0");
                }
                break;
            case 1:
                {
                    //  Blue.
                    this.GetComponent<Renderer>().material = Resources.Load<Material>("1");
                }
                break;
            case 2:
                {
                    //  Yellow.
                    this.GetComponent<Renderer>().material = Resources.Load<Material>("2");
                }
                break;
            case 3:
                {
                    //  Purple.
                    this.GetComponent<Renderer>().material = Resources.Load<Material>("3");
                }
                break;
            default:
                {

                }
                break;
        }
    }

    public void Initialize(Quaternion _Rotation)
    {
        this.transform.rotation = _Rotation;
        m_vDirection = new Vector3(0.0f, 1.0f, 0.0f);
        m_fSpeed = 10.0f;
        m_fDecay = 0.9f;
    }
}
