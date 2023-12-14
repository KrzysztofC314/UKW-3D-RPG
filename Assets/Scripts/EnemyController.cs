using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] private CharacterSheet characterSheet;

    void Start()
    {
        currentHealth = characterSheet.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Wywo�ane w momencie �mierci wroga
        Destroy(gameObject);
    }
}

