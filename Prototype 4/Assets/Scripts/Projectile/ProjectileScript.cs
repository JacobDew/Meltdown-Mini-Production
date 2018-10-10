using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector3 m_vDirection = new Vector3(0.0f, 0.0f, 0.0f);

    private List<GameObject> m_pHitList = new List<GameObject>();

    private float m_fSpeed;
    private float m_fDamage;
    private int m_iPierce;


    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f != m_vDirection.x || 0.0f != m_vDirection.z)
        {
            this.transform.position += m_vDirection * m_fSpeed * Time.deltaTime;
        }
    }

    //  When projectile goes offscreen it deletes itself.
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Follower")
        {
            if (true == m_pHitList.Contains(other.gameObject))
            {
                Debug.Log("True");
                return;
            }
            m_iPierce -= 1;
            other.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(m_fDamage);
            if (0 > m_iPierce)
            {
                Destroy(gameObject);
            }
            m_pHitList.Add(other.gameObject);
        }
    }

    public void Initialize(Vector3 _Direction, float _Damage, float _Speed, int _Pierce, int _Multishot = 0)
    {
        this.transform.position += _Direction;
        m_vDirection = _Direction;
        m_fDamage = _Damage;
        m_fSpeed = _Speed;
        m_iPierce = _Pierce;
        switch (_Multishot)
        {
            case 0:
                {

                }
                break;
            default:
                {
                    int Rotation = 0 - _Multishot;
                    GameObject TempProjectile;
                    //  Rotate about Y and spawn more.
                    for (int iX = 0; iX < _Multishot; iX++)
                    {
                        TempProjectile = Instantiate(this.gameObject);
                        TempProjectile.GetComponent<ProjectileScript>().Initialize(Quaternion.Euler(0, Rotation, 0) * _Direction, _Damage, _Speed, _Pierce);
                        Rotation += 2;
                    }
                }
                break;
        }
    }


}