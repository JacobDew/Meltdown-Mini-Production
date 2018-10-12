using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Vector3 m_vDirection;
    private float m_fSpeed;
    private float m_fDecay;
    private int m_iMode = 0;
    private float m_fLifetime;

    private float m_fDeltaTime = 0.0f;


    // Use this for initialization
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        m_fDeltaTime = Time.deltaTime;
        switch (m_iMode)
        {
            case 0:
                {
                    this.transform.Translate(m_vDirection * m_fSpeed * m_fDeltaTime);
                    this.transform.localScale *= 1.0f - m_fDeltaTime;
                    if (0.01f > m_fSpeed)
                    {
                        Destroy(gameObject);
                    }
                    m_fSpeed *= m_fDecay * 1.0f + m_fDeltaTime;
                }
                break;
            case 1:
                {
                    this.transform.Translate(m_vDirection * m_fSpeed * m_fDeltaTime);
                    this.transform.localScale *= 1.0f + m_fDecay * m_fDeltaTime;
                    m_fLifetime -= m_fDeltaTime;
                    if (0.0001f > m_fLifetime)
                    {
                        Destroy(gameObject);
                    }
                }
                break;
            default:
                {

                }
                break;
        }
	}

    public void Initialize(Quaternion _Rotation, Vector3 _Direction, int _Material, float _Speed = 40.0f, float _Decay = 0.5f)
    {
        m_vDirection = _Direction;
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

    public void Initialize(Vector3 _Direction, float _Speed = 40.0f, float _Decay = 0.5f, int _Mode = 0, float _Lifetime = 2.0f)
    {
        m_vDirection = _Direction;
        m_fSpeed = _Speed;
        m_fDecay = _Decay;
        m_iMode = _Mode;
        m_fLifetime = _Lifetime;
    }

    public void Initialize(Quaternion _Rotation)
    {
        this.transform.rotation = _Rotation;
        m_vDirection = new Vector3(0.0f, 1.0f, 0.0f);
        m_fSpeed = 10.0f;
        m_fDecay = 0.9f;
    }
}
