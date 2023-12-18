using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public healtBar HealtBar;
    public bool isDie;

    void Start()
    {
        LoadPlayerHealth(); // Try loading the player health at the start
    }

    void Update()
    {
        // Your update logic here
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealtBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDie = true;
            Debug.Log("Player is dead!");

            // Respawn the player at the last save spot
            SaveSpot.RespawnPlayer(gameObject);

            // Save the current health before reloading the scene
            SavePlayerHealth();

            // Reload the scene when the player dies
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        HealtBar.SetHealth(currentHealth);

        // Save the current health after healing
        SavePlayerHealth();
    }

    void SavePlayerHealth()
    {
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }

    void LoadPlayerHealth()
    {
        currentHealth = PlayerPrefs.GetInt("PlayerHealth", maxHealth);
        HealtBar.SetMaxHealth(maxHealth);
        HealtBar.SetHealth(currentHealth);

        // Print the loaded health value for debugging
        Debug.Log("Loaded Health: " + currentHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
    }
}
