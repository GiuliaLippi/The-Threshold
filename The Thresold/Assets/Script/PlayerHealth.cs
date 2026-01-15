using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public float heartValue = 100f;

    public float currentHealth;
    public float halfLastHeart;

    void Start()
    {
        currentHealth = maxHearts * heartValue;
        halfLastHeart = heartValue / 2f;
    }

    public bool IsCritical()
    {
        return currentHealth <= halfLastHeart;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (IsCritical())
            GameEvents.OnPlayerCritical?.Invoke();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        GameEvents.OnPlayerDeath?.Invoke();
    }

    public void RestoreHalfLife()
    {
        currentHealth = halfLastHeart;
    }
}
