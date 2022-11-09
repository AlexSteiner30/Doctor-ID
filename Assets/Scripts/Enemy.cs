using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")] 
    public float startingHealth;
    private float currentHealth;
    
    void Start()
    {
        currentHealth = startingHealth;
    }
    
    public void ShootOn(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this);
    }
}
