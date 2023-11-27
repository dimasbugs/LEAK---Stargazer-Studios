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
        // Simulate taking damage with space bar (you can replace this with your actual damage logic)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1); // Adjust the damage amount as needed
        }
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
}
