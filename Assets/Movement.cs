using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        isDialogue = teamController.isDialogue;
        isMenu = gameManager.isMenu;
        isTeam = gameManager.isTeam;
        if (!isDialogue&&!isMenu) 
        {
            if (!isTeam&&gameManager.ActivePlayer == placeInTeam)
            {
                ChosenPlayerMovement();
            }
            else if(isTeam)
            {
                FollowPlayerMovement();
            }
        }
    }

    private void ChosenPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
    private void FollowPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    switch (placeInTeam)
                    {
                        case 1:
                            agent.SetDestination(hit.point + new Vector3(stopDistance, 0, stopDistance));
                            break;
                        case 2:
                            agent.SetDestination(hit.point + new Vector3(-stopDistance, 0, stopDistance));
                            break;
                        case 3:
                            agent.SetDestination(hit.point + new Vector3(stopDistance, 0, -stopDistance));
                            break;
                        case 4:
                            agent.SetDestination(hit.point + new Vector3(-stopDistance, 0, -stopDistance));
                            break;
                    }
                }
            }
        }
    }

    

}
