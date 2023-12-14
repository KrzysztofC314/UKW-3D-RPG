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
    [SerializeField] private int MaxSpeed = 10;
    private int CurrentSpeed;
    [SerializeField] private int MaxAttackPoints = 1;
    private int AttackPoints;
    [SerializeField] private int Damage = 3;
    [SerializeField] private int AttackRange = 25;

    private NavMeshAgent agent;
    private bool hasAttacked = false;

    void Start()
    {
        currentHealth = characterSheet.MaxHealth;
        CurrentSpeed = MaxSpeed;
        AttackPoints = MaxAttackPoints;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = MaxSpeed;
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
            CurrentSpeed = MaxSpeed;
            AttackPoints = MaxAttackPoints;
        }

            if (gameManager.isFight && characterSheet.PlayerTurn == gameManager.Turn)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Transform nearestPlayer = null;
            float nearestDistance = Mathf.Infinity;

            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPlayer = player.transform;
                }
            }

            if (nearestPlayer != null && nearestDistance <= AttackRange)
            {
                if (agent != null && agent.isOnNavMesh && agent.isStopped && AttackPoints > 0)
                {
                    float distanceToPlayer = Mathf.Min(nearestDistance, CurrentSpeed);
                    Vector3 destination = transform.position + (nearestPlayer.position - transform.position).normalized * distanceToPlayer;
                    agent.SetDestination(destination);

                    // Sprawdzanie zasiêgu ataku po osi¹gniêciu docelowego punktu
                    if (distanceToPlayer <= AttackRange)
                    {
                        AttackPlayer(nearestPlayer);
                        AttackPoints--;
                    }
                }
            }
        }
    }

    void AttackPlayer(Transform player)
    {
        for (int i = 0; i < AttackPoints; i++)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, AttackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Movement playerMovement = hit.collider.GetComponent<Movement>();
                    if (playerMovement != null)
                    {
                        playerMovement.TakeDamage(Damage);
                        Debug.Log("Enemy dealt " + Damage + " damage to the player.");
                    }
                }
            }
            else
            {
                Debug.Log("Enemy raycast didn't hit the player.");
            }

            Debug.DrawRay(transform.position, direction * AttackRange, Color.green, 1.0f);
        }
    }

}

