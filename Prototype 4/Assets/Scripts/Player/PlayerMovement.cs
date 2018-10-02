using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Plane playerPlane;
    public Transform Player;
    public Ray ray;
    public float playerSpeed;

    void Start()
    {

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
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = targetRotation;
            
        }
        Movement();
    }

    private void Movement()
    {
        if (true == Input.GetKey(KeyCode.D) && true == Input.GetKey(KeyCode.A))
        {
            if (true == Input.GetKey(KeyCode.W))
            {
                //  Up movement.
                this.transform.position += new Vector3(0.7071f, 0.0f, 0.7071f) * playerSpeed * Time.deltaTime;
            }
            else if (true == Input.GetKey(KeyCode.S))
            {
                //  Down movement.
                this.transform.position += new Vector3(-0.7071f, 0.0f, -0.7071f) * playerSpeed * Time.deltaTime;
            }
        }
        else if (true == Input.GetKey(KeyCode.W) && true == Input.GetKey(KeyCode.S))
        {
            if (true == Input.GetKey(KeyCode.D))
            {
                //  Right movement.
                this.transform.position += new Vector3(0.7071f, 0.0f, -0.7071f) * playerSpeed * Time.deltaTime;
            }
            else if (true == Input.GetKey(KeyCode.A))
            {
                //  Left movement.
                this.transform.position += new Vector3(-0.7071f, 0.0f, 0.7071f) * playerSpeed * Time.deltaTime;
            }
        }
        else if (true == Input.GetKey(KeyCode.D) && true == Input.GetKey(KeyCode.W))
        {
            //  Right Up movement.
            this.transform.position += new Vector3(1.0f, 0.0f, 0.0f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.W) && true == Input.GetKey(KeyCode.A))
        {
            //  Up Left movement.
            this.transform.position += new Vector3(0.0f, 0.0f, 1.0f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.A) && true == Input.GetKey(KeyCode.S))
        {
            //  Left Down movement.
            this.transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.S) && true == Input.GetKey(KeyCode.D))
        {
            //  Down Right movement.
            this.transform.position += new Vector3(0.0f, 0.0f, -1.0f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.D))
        {
            //  Right movement.
            this.transform.position += new Vector3(0.7071f, 0.0f, -0.7071f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.W))
        {
            //  Up movement.
            this.transform.position += new Vector3(0.7071f, 0.0f, 0.7071f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.A))
        {
            //  Left movement.
            this.transform.position += new Vector3(-0.7071f, 0.0f, 0.7071f) * playerSpeed * Time.deltaTime;
        }
        else if (true == Input.GetKey(KeyCode.S))
        {
            //  Down movement.
            this.transform.position += new Vector3(-0.7071f, 0.0f, -0.7071f) * playerSpeed * Time.deltaTime;
        }
        else
        {

        }
    }


}
