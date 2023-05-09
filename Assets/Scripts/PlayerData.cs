using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            HealthSystem();
        }

        if (collision.gameObject.tag == "Spike")
        {
            GameManager.Instance.Respawn();
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;
        }
        else if (collision.gameObject.tag == "Rock")
        {
            HealthSystem();
        }
        else if (collision.gameObject.tag == "DeathBox")
        {
            GameManager.Instance.Respawn();
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;
        }
        else if (collision.gameObject.tag == "BoobyTrap")
        {
            GameManager.Instance.Respawn();
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth;
        }

    }

    public void HealthSystem()
    {
        GameManager.Instance.currentHealth -= 25; // Reduce health by 25
        if (GameManager.Instance.currentHealth <= 0)
        {
            GameManager.Instance.currentHealth = 0; // Prevent health from going below 0
            GameManager.Instance.Respawn();
            GameManager.Instance.currentHealth = GameManager.Instance.maxHealth; // Reset health to maxHealth after respawning
        }
        else
        {
            Debug.Log("Player health: " + GameManager.Instance.currentHealth);
        }
    }

}

