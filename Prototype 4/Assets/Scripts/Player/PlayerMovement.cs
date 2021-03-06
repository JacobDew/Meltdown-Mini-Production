﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;

    private Vector3 m_vUp = new Vector3(0.7071f, 0.0f, 0.7071f);
    private Vector3 m_vDown = new Vector3(-0.7071f, 0.0f, -0.7071f);
    private Vector3 m_vLeft = new Vector3(-0.7071f, 0.0f, 0.7071f);
    private Vector3 m_vRight = new Vector3(0.7071f, 0.0f, -0.7071f);

    private Vector3 m_vUpLeft = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 m_vUpRight = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 m_vDownLeft = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 m_vDownRight = new Vector3(0.0f, 0.0f, -1.0f);



    public Plane playerPlane;
    public Transform Player;
    public Transform PlayerTopHalf;
    public Ray ray;
    public float playerSpeed;

    void Start()
    {
        m_Animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPlane = new Plane(Vector3.up, transform.position);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - PlayerTopHalf.transform.position);

            PlayerTopHalf.transform.transform.rotation = targetRotation;
           // Player.transform.rotation = targetRotation;

        }
        Movement();
    }

    private void Movement()
    {
        if (true == Input.GetKey(KeyCode.D) && true == Input.GetKey(KeyCode.A))
        {
            m_Animator.SetFloat("Input", 0.5f);
            if (true == Input.GetKey(KeyCode.W))
            {
                m_Animator.SetFloat("Input", 0.5f);
                //  Up movement.
                this.transform.position += m_vUp * playerSpeed * Time.deltaTime;
            }
            else if (true == Input.GetKey(KeyCode.S))
            {
                m_Animator.SetFloat("Input", 0.5f);
                //  Down movement.
                this.transform.position += m_vDown * playerSpeed * Time.deltaTime;
            }
        }
        else if (true == Input.GetKey(KeyCode.W) && true == Input.GetKey(KeyCode.S))
        {
            m_Animator.SetFloat("Input", 0.5f);
            if (true == Input.GetKey(KeyCode.D))
            {
                m_Animator.SetFloat("Input", 0.5f);
                //  Right movement.
                this.transform.position += m_vRight * playerSpeed * Time.deltaTime;
            }
            else if (true == Input.GetKey(KeyCode.A))
            {
                m_Animator.SetFloat("Input", 0.5f);
                //  Left movement.
                this.transform.position += m_vLeft * playerSpeed * Time.deltaTime;
            }
        }
        else if (true == Input.GetKey(KeyCode.D) && true == Input.GetKey(KeyCode.W))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Up Right movement.
            this.transform.position += m_vUpRight * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.W) && true == Input.GetKey(KeyCode.A))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Up Left movement.
            this.transform.position += m_vUpLeft * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.A) && true == Input.GetKey(KeyCode.S))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Down Left movement.
            this.transform.position += m_vDownLeft * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.S) && true == Input.GetKey(KeyCode.D))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Down Right movement.
            this.transform.position += m_vDownRight * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.D))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Right movement.
            this.transform.position += m_vRight * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.W))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Up movement.
            this.transform.position += m_vUp * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.A))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Left movement.
            this.transform.position += m_vLeft * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.S))
        {
            m_Animator.SetFloat("Input", 0.5f);
            //  Down movement.
            this.transform.position += m_vDown * playerSpeed * Time.deltaTime;
        }
        else
        {
            m_Animator.SetFloat("Input", 0.0f);
        }

    }


}
