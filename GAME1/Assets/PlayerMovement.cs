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
    private bool isHiding = false;
    private Vector3 hidingStartPosition;

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

        // Check for interaction with hiding spots
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
            {
                EndHiding();
            }
            else
            {
                TryToHide();
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

    void TryToHide()
    {
        // Raycast to check if there is a hiding spot in front of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, 1.5f, LayerMask.GetMask("HidingSpot"));

        if (hit.collider != null)
        {
            StartHiding();
        }
    }

    void StartHiding()
    {
        isHiding = true;
        hidingStartPosition = transform.position;

        // Deactivate components and make the player invisible
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        // Add more components to deactivate as needed

        // Optionally perform additional hiding actions, e.g., play hiding animation
    }

    void EndHiding()
    {
        // Reactivate components and make the player visible
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        // Add more components to reactivate as needed

        // Reappear the player at the last hiding position
        transform.position = hidingStartPosition;

        // Optionally perform additional actions after ending hiding, e.g., play end hiding animation

        isHiding = false;
    }

}
