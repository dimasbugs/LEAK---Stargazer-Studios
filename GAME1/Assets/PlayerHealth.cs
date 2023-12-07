using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public healtBar HealtBar;

    void Start()
    {
        currentHealth = maxHealth;
        HealtBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealtBar.SetHealth(currentHealth);

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is dead!");
            // You can add a game over logic here or respawn the player.
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Ensure that current health does not exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Reduce player health by 1 when colliding with an object with "Enemy" tag
            TakeDamage(1);
        }
    }
}
