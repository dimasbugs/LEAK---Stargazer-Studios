using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    public Animator animator; 
    private bool isSprinting = false;


    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        Sprinting();
    }

    private void FixedUpdate()

    {

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

    }



    void Sprinting()

    {

        if (Input.GetKey(KeyCode.RightShift))

        {

            isSprinting = true;

        }

        else

        {

            isSprinting = false;

        }



        if (isSprinting)

        {

            moveSpeed = 1f;

        }

        else

        {

            moveSpeed = 0.5f;

        }
    }
    }
