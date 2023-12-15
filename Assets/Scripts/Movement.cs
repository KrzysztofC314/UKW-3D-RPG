using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Movement : MonoBehaviour
{
    public Camera camera;
    [SerializeField] GameManager gameManager;
    [SerializeField] private CharacterSheet characterSheet;
    private RaycastHit hit;
    private NavMeshAgent agent;
    private string groundTag = "Ground";
    [SerializeField] private float stopDistance;
    [SerializeField] private TeamController teamController;

    [SerializeField] private int placeInTeam;
    public bool isWalking;
    public bool isAttack;
    private Vector3 destination;
    private float speed;

    [SerializeField] private TMP_Text EPCost;

    private bool isFight;
    private int moveCost;
    private bool isCostGreaterThanEnergy;
    private int energyCost;

    private float attackRange = 25f; // Zasi�g ataku
    private int damage = 30;
    public bool isDead;


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
        //A tu si� to g�wno od rozdzielania ko�czy
    }

    // Update is called once per frame
    void Update()
    {
        isFight = gameManager.isFight;
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        //characterSheet.Energy;
        EPCost.enabled = isFight;

        if (speed >= 0.1)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (characterSheet.Health == 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
        if (characterSheet.Health > 0)
        {
            isDead = false;
        }

        if (isDead == false)
        {
            if (gameManager.isDialog == false && gameManager.isMenu == false && gameManager.Action == 1)
            {
                if (gameManager.isTeam == false && gameManager.ActivePlayer == placeInTeam && isFight == false)
                {
                    ChosenPlayerMovement();
                    //isAttack = false;
                }
                else if (gameManager.isTeam == true && isFight == false)
                {
                    FollowPlayerMovement();
                    //isAttack = false;
                }
                else if (gameManager.Turn == characterSheet.PlayerTurn && isFight == true && gameManager.Action == 1)
                {
                    FightPlayerMovement();
                    //isAttack = false;

                    if (!IsPointerOverUI())
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                        {
                            if (hit.collider.CompareTag(groundTag))
                            {
                                Vector3 currentMousePosition = hit.point;

                                // Obliczamy odleg�o�� mi�dzy aktualn� pozycj� myszy a pozycj� postaci
                                float distance = Vector3.Distance(transform.position, currentMousePosition);
                                int energyCost = Mathf.CeilToInt(distance); // Zaokr�glamy do g�ry

                                UpdateEPCostText(energyCost); // Aktualizujemy tekst z kosztem energii
                            }
                        }
                    }
                }
            }
        }
    }
    //podstawowy skrypt od pod��ania pojedy�czej postaci
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
    // skrypt od poruszania si� w grupie - po kontakcie raycasta z ziemi� skrypt kalkuluje pozycj� postaci w zale�no�ci od zmiennej placeInTeam
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

    /* tu to jest stare - NIE DOTYKA�!!!
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
    */

    private void FightPlayerMovement() // poruszanie postaci w stanie walki 
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    Vector3 previousDestination = agent.destination; // zapisujemy poprzedni� pozycj�
                    agent.SetDestination(hit.point);

                    // obliczamy odleg�o�� mi�dzy poprzedni� a now� pozycj�
                    float distance = Vector3.Distance(previousDestination, hit.point);
                    int energyCost = Mathf.CeilToInt(distance); //zaokr�glamy do g�ry

                    // sprawdzamy, czy mamy wystarczaj�c� ilo�� energii przed ruchem
                    if (characterSheet.Energy >= energyCost)
                    {
                        characterSheet.Energy -= energyCost; // odejmujemy zu�yt� energi�
                        UpdateEPCostText(energyCost); //aktualizujemy tekst z kosztem energii
                    }
                    else
                    {
                        // Je�li nie mamy wystarczaj�cej ilo�ci energii, anulujemy ruch.
                        agent.ResetPath(); // resetujemy trase aby posta� nie ruszy�a si� do celu
                        Debug.Log("Niewystarczaj�ca ilo�� energii!");
                    }
                }
            }
        }
    }

    private void UpdateEPCostText(int cost) //updatujemy koszt ruchu w tek�cie
    {
        if (cost > characterSheet.Energy)
        {
            EPCost.color = Color.red; // Ustawienie koloru tekstu na czerwony, je�li koszt jest wi�kszy od energii
        }
        else
        {
            EPCost.color = new Color(0.53f, 0.81f, 0.98f); // Ustawienie ja�niejszego niebieskiego koloru tekstu
        }
        EPCost.text = $"Energy Cost: {cost}";
    }


    private bool IsPointerOverUI() //sprawdza czy myszka jest na ui
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void TakeDamage(int damage)
    {
        characterSheet.Health -= damage;
        if (characterSheet.Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
