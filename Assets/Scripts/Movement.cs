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

    private bool isFight;
    private int Energy;

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
        isFight = gameManager.isFight;
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        Energy = characterSheet.Energy;

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

                if (gameManager.isTeam == false && gameManager.ActivePlayer == placeInTeam && isFight == false)
                {
                    ChosenPlayerMovement();
                isAttack = false;
                }
                else if (gameManager.isTeam == true && isFight == false)
                {
                    FollowPlayerMovement();
                isAttack = false;
            }
            else if (gameManager.Turn == characterSheet.PlayerTurn && isFight == true && gameManager.Action == 1)
            {
                FightPlayerMovement();
                isAttack = false;
            }
            else if (gameManager.Turn == characterSheet.PlayerTurn && isFight == true && gameManager.Action == 2)
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

    public void FightPlayerAttack()
    {


    }


    private bool IsPointerOverUI() //sprawdza czy myszka jest na ui
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
