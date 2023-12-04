using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public Camera camera;
    [SerializeField] GameManager gameManager;
    private RaycastHit hit;
    private NavMeshAgent agent;
    private string groundTag = "Ground";
    [SerializeField] private float stopDistance;
    [SerializeField] private TeamController teamController;


    private string enemyTag = "Enemy";
    public float attackRange = 25f;
    public float attackDamage = 5f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private Transform targetEnemy;
    public float attackInterval = 2f; //czas pomiêdzy atakami
    private float attackTimer = 0f;


    [SerializeField] private int placeInTeam;
    public bool isWalking;
    public bool isAttack;
    private Vector3 destination;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        //Tu jest to od rozdzielania 
        gameManager.Player1.onClick.AddListener(() =>
        {
            gameManager.isTeam = false;
        });
        gameManager.Player2.onClick.AddListener(() =>
        {
            gameManager.isTeam = false;
        });
        gameManager.Player3.onClick.AddListener(() =>
        {
            gameManager.isTeam = false;
        });
        gameManager.Player4.onClick.AddListener(() =>
        {
            gameManager.isTeam = false;
        });
        //A tu siê to gówno od rozdzielania koñczy
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        if (speed >= 0.1)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (gameManager.isDialog == false && gameManager.isMenu == false && gameManager.Action == 1)
        {

                if (gameManager.isTeam == false && gameManager.ActivePlayer == placeInTeam && gameManager.isFight == false)
                {
                    ChosenPlayerMovement();
                isAttack = false;
                }
                else if (gameManager.isTeam == true && gameManager.isFight == false)
                {
                    FollowPlayerMovement();
                isAttack = false;
            }
            else if (gameManager.ActivePlayer == placeInTeam && gameManager.isFight == true && gameManager.Action == 1)
            {
                FightPlayerMovement();
                isAttack = false;
            }
            else if (gameManager.ActivePlayer == placeInTeam && gameManager.Action == 2)
            {
                FightPlayerAttack();
            }
        }
    }
    //podstawowy skrypt od pod¹¿ania pojedyñczej postaci
    private void ChosenPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    destination = hit.point;
                    agent.SetDestination(destination);
                }
            }
        }
    }
    // skrypt od poruszania siê w grupie - po kontakcie raycasta z ziemi¹ skrypt kalkuluje pozycjê postaci w zale¿noœci od zmiennej placeInTeam
    private void FollowPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI() && gameManager.isFight == false)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    switch (placeInTeam)
                    {
                        case 1:
                            destination = hit.point + new Vector3(stopDistance, 0, stopDistance);
                            break;
                        case 2:
                            destination = hit.point + new Vector3(-stopDistance, 0, stopDistance);
                            break;
                        case 3:
                            destination = hit.point + new Vector3(stopDistance, 0, -stopDistance);
                            break;
                        case 4:
                            destination = hit.point + new Vector3(-stopDistance, 0, -stopDistance);
                            break;
                    }
                    agent.SetDestination(destination);
                }
            }
        }
    }
    private void FightPlayerMovement() // poruszanie postaci w stanie walki 
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    destination = hit.point;
                    agent.SetDestination(destination);
                }
            }
        }
    }

    private void FightPlayerAttack()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange, enemyLayer))
            {
                if (hit.collider.CompareTag(enemyTag))
                {
                    isAttack = true;
                    targetEnemy = hit.transform;
                }
            }
        }

        if (isAttack && attackTimer <= 0)
        {
            Attack();
            attackTimer = attackInterval; // Resetuj timer ataku
        }
        else
        {
            StopAttack();
        }
    }

    void Attack()
    {
        // Obróæ postaæ w kierunku wroga
        Vector3 direction = targetEnemy.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Wykonaj raycast w kierunku wroga
        RaycastHit enemyHit;
        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out enemyHit, attackRange, enemyLayer))
        {
            if (enemyHit.collider.CompareTag(enemyTag))
            {
                // Zadaj obra¿enia
                DealDamage(enemyHit.collider);
            }
        }
        // SprawdŸ czy wrogowi skoñczy³o siê zdrowie
        EnemyHealth enemyHealth = targetEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null && enemyHealth.currentHealth <= 0)
        {
            StopAttack();
            return;
        }
    }

    void DealDamage(Collider enemyCollider)
    {
        
        Debug.Log("Zadano obra¿enia wrogowi!");

        // tu wstawiæ zabieranie dmg np.
        EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }

    void StopAttack()
    {
        // Przestañ atakowaæ
        isAttack = false;
    }



    private bool IsPointerOverUI() //sprawdza czy myszka jest na ui
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
