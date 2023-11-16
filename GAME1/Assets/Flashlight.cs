using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public float lightRadius = 5f;
    public Transform playerTransform; // Reference to the player's transform
    public float minLightIntensity = 0.1f;
    public float maxLightIntensity = 1f;
    public Light flashlightLight; // Reference to the Light component

    private bool isFlashlightOn = true; // Initial state is on
    private SpriteRenderer flashlightSpriteRenderer; // Reference to the sprite renderer

    void Start()
    {
        FindPlayer();
        FindFlashlightSpriteRenderer();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            FindPlayer();
        }

        if (flashlightSpriteRenderer == null)
        {
            FindFlashlightSpriteRenderer();
        }

        if (playerTransform != null)
        {
            // Toggle flashlight on/off when the "F" key is pressed
            if (Input.GetKeyDown(KeyCode.F))
            {
                isFlashlightOn = !isFlashlightOn;
                flashlightLight.enabled = isFlashlightOn; // Turn on/off the light component

                // Check if the sprite renderer reference is assigned before using it
                if (flashlightSpriteRenderer != null)
                {
                    flashlightSpriteRenderer.enabled = isFlashlightOn; // Turn on/off the sprite renderer
                }
                else
                {
                    Debug.LogError("Flashlight SpriteRenderer not found!");
                }
            }

            // Get mouse position in world space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate direction from player to mouse
            Vector2 direction = (mousePosition - (Vector2)playerTransform.position).normalized;

            // Cast a ray in the direction of the mouse
            RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, direction, lightRadius, LayerMask.GetMask("Foreground"));

            // Update flashlight position and rotation
            transform.position = (Vector2)playerTransform.position + direction * lightRadius * 0.5f;
            transform.right = direction;

            // If the ray hits an obstacle, adjust the light radius
            if (hit.collider != null)
            {
                lightRadius = Vector2.Distance(transform.position, hit.point);
            }
            else
            {
                lightRadius = 2.6f; // Set default light radius if no obstacle is hit
            }
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player transform not found!");
        }
    }

    void FindFlashlightSpriteRenderer()
    {
        flashlightSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (flashlightSpriteRenderer == null)
        {
            Debug.LogError("Flashlight SpriteRenderer not found!");
        }
    }
}
