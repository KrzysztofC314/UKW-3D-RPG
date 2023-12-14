using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] private CharacterSheet characterSheet;
    [SerializeField] private GameManager gameManager;
    private bool isDead = false;
    [SerializeField] private int MaxMoveDistance = 10;
    private int MoveDistance;
    [SerializeField] private int MaxAttackPoints = 1;
    private int AttackPoints;
    [SerializeField] private int Damage = 3;
    [SerializeField] private int AttackRange = 25;

    private NavMeshAgent agent;

    void Start()
    {
        currentHealth = characterSheet.MaxHealth;
        AttackPoints = MaxAttackPoints;
        MoveDistance = MaxMoveDistance;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = 5;
        }
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
        Destroy(gameObject);
        isDead = true;
    }

    void Update()
    {
        if (gameManager.isFight == false || characterSheet.PlayerTurn != gameManager.Turn)
        {
            MoveDistance = MaxMoveDistance;
            AttackPoints = MaxAttackPoints;
        }

        if (gameManager.isFight && characterSheet.PlayerTurn == gameManager.Turn)
        {
           
        }
    }

    void Moving()
    {

    }

    void Attack()
    {

    }
}

