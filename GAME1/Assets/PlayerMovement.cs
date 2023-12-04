using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isWalking = true;
    private bool isSprinting = false;

    [SerializeField]
    private Button PauseButton;

    [SerializeField]
    private Button resumeButton;

    private bool isPaused = false;


    Vector2 movement;
    Vector3 moveDir;

    void Start()
    {
        // Add a listener for the resume button click event
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }


    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Optionally, you can do other things when the game is unpaused, like hiding the pause menu UI
    }

    void PauseGame()
    {
        // Pause the game logic here
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game

        // Optionally, you can do other things when the game is paused, like showing the pause menu UI
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0  || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            if (!isWalking)
            {
                isWalking = true;
                animator.SetBool("isMoving", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                animator.SetBool("isMoving", isWalking);
                StopMoving();
            }
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
        Sprinting();

        // Get a reference to the flashlight script
        Flashlight flashlight = GetComponentInChildren<Flashlight>();

        if (flashlight != null)
        {
            // Set the player transform in the flashlight script
            flashlight.playerTransform = transform;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }


        void TogglePause()
        {
            if (PauseButton != null)
                PauseButton.onClick.Invoke();
            {
                isPaused = !isPaused;

                if (isPaused)
                {
                    PauseGame();
                }
                else
                {
                    // Don't need to call UnpauseGame here, as it's handled by the "Resume" button click
                }
            }
        }

        moveDir = new Vector3(movement.x, movement.y).normalized;
    }

    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
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
