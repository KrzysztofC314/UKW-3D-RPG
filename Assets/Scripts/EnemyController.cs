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
    public int MoveDistance;
    [SerializeField] private int MaxAttackPoints = 1;
    public int AttackPoints;
    [SerializeField] public int Damage = 3;
    [SerializeField] public int AttackRange = 25;
    public bool IsInRange = false;
    private string playerTag = "Player";
    public bool isAttack = false;


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

        //-------------------------------
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        // Domyœlnie ustaw IsInRange na false
        IsInRange = false;

        // Dla ka¿dego znalezionego obiektu "Player"
        foreach (GameObject player in players)
        {
            // Oblicz odleg³oœæ miêdzy obiektem a graczem
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Jeœli odleg³oœæ jest mniejsza ni¿ AttackRange, ustaw IsInRange na true
            if (distance < AttackRange)
            {
                IsInRange = true;
            }
        }
        //---------------------------------------

        if (gameManager.isFight && characterSheet.PlayerTurn == gameManager.Turn)
        {
            if (IsInRange == true)
            {
                isAttack = true;
            }
            if (IsInRange == false && MoveDistance > 0)
            {
                Moving();
            }
            if (AttackPoints == 0)
            {
                gameManager.Turn++;
            }
            if (MoveDistance == 0 && IsInRange == false)
            {
                gameManager.Turn++;
            }
        }
    }

    void Moving()
    {

    }
}