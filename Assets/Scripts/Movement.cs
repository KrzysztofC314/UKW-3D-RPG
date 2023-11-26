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
    private bool isDialogue;
    private bool isMenu;
    private bool isTeam;
    [SerializeField] private int placeInTeam;
    private AnimController animController;
    private Vector3 location;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponentInChildren<AnimController>();


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
        location = transform.position;
        if (location != destination)
        {
            animController.isWalking = false;
        }
        else
        {
            animController.isWalking= true;
        }
        isDialogue = teamController.isDialogue;
        isMenu = gameManager.isMenu;
        isTeam = gameManager.isTeam;
        if (!isDialogue&&!isMenu) 
        {
            if (!isTeam && gameManager.ActivePlayer == placeInTeam)
            {
                ChosenPlayerMovement();
            }
            else if(isTeam)
            {
                FollowPlayerMovement();
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
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
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
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


}
