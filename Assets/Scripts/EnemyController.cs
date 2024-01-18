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

        // Domy�lnie ustaw IsInRange na false
        IsInRange = false;

        // Dla ka�dego znalezionego obiektu "Player"
        foreach (GameObject player in players)
        {
            // Oblicz odleg�o�� mi�dzy obiektem a graczem
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Je�li odleg�o�� jest mniejsza ni� AttackRange, ustaw IsInRange na true
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
        if (isDead)
        {
            // Je�li przeciwnik jest martwy, zako�cz funkcj� poruszania
            return;
        }

        // Znajd� wszystkich graczy w scenie
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        // Znajd� najbli�szego gracza
        GameObject nearestPlayer = FindNearestPlayer(players);

        // Je�li nie ma gracza w zasi�gu ruchu, zako�cz funkcj� poruszania
        if (nearestPlayer == null)
        {
            return;
        }

        // Ustaw cel poruszania na pozycj� gracza
        agent.SetDestination(nearestPlayer.transform.position);

        // Zmniejsz MoveDistance o odleg�o�� do celu
        MoveDistance -= (int)Vector3.Distance(transform.position, nearestPlayer.transform.position);

        // Oznacz atak, je�li przeciwnik jest w zasi�gu ataku
        if (IsInRange && AttackPoints > 0)
        {
            isAttack = true;
        }

        // Je�li MoveDistance jest mniejsze lub r�wne 0, zako�cz ruch i zmie� tur�
        if (MoveDistance <= 0)
        {
            gameManager.Turn++;
        }
    }

    GameObject FindNearestPlayer(GameObject[] players)
    {
        GameObject nearestPlayer = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < nearestDistance)
            {
                nearestPlayer = player;
                nearestDistance = distance;
            }
        }

        return nearestPlayer;
    }
}
