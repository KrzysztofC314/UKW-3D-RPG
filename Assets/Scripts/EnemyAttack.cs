using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private string playerTag = "Player";



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.isAttack == true && enemyController.AttackPoints > 0)
        {
            // Pocz¹tkowo zak³adamy brak najbli¿szego gracza
            GameObject nearestPlayer = null;
            float nearestDistance = Mathf.Infinity;

            // ZnajdŸ wszystkie obiekty graczy
            GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

            // Dla ka¿dego gracza oblicz odleg³oœæ
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);

                // Jeœli odleg³oœæ jest mniejsza ni¿ poprzednia najkrótsza odleg³oœæ, zaktualizuj gracza i odleg³oœæ
                if (distance < nearestDistance)
                {
                    nearestPlayer = player;
                    nearestDistance = distance;
                }
            }

            // Jeœli znaleziono najbli¿szego gracza, wykonaj atak
            if (nearestPlayer != null)
            {
                // Wystrzel raycast w kierunku najbli¿szego gracza
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (nearestPlayer.transform.position - transform.position).normalized, out hit, enemyController.AttackRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Movement movementScript = hit.collider.GetComponent<Movement>();

                        if (movementScript != null)
                        {
                            movementScript.TakeDamage(enemyController.Damage);
                            Debug.Log("trafienie");
                            enemyController.AttackPoints--;
                        }
                    }
                }
            }
        }
    }
}
