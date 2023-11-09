using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [HideInInspector] public bool isChosen = false;
    public Camera camera;
    private RaycastHit hit;
    private NavMeshAgent agent;
    private string groundTag = "Ground";
    [SerializeField] private float stopDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (isChosen)
        {
            ChosenPlayerMovement();
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

    //private Vector3 CalculateDestination(Vector3 target)
    //{
    //    int playersCount = gameManager.isTeam ? 4 : allowedPlayers.Count;
    //    Vector3[] offsets = new Vector3[4];

    //    if (playersCount == 2)
    //    {
    //        offsets[0] = new Vector3(-stopDistance, 0, 0);
    //        offsets[1] = new Vector3(stopDistance, 0, 0);
    //    }
    //    else if (playersCount == 3)
    //    {
    //        offsets[0] = new Vector3(-stopDistance, 0, stopDistance);
    //        offsets[1] = new Vector3(stopDistance, 0, stopDistance);
    //        offsets[2] = new Vector3(0, 0, -stopDistance);
    //    }
    //    else if (playersCount == 4)
    //    {
    //        offsets[0] = new Vector3(-stopDistance, 0, stopDistance);
    //        offsets[1] = new Vector3(stopDistance, 0, stopDistance);
    //        offsets[2] = new Vector3(-stopDistance, 0, -stopDistance);
    //        offsets[3] = new Vector3(stopDistance, 0, -stopDistance);
    //    }

    //    Vector3 offset = gameManager.isTeam ? offsets[(int)thisPlayer] : offsets[allowedPlayers.IndexOf(thisPlayer)];
    //    var direction = target - transform.position;
    //    var angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
    //    offset = RotateVector(offset, angle);

    //    return target + offset;
    //}

    //private Vector3 RotateVector(Vector3 v, float angle)
    //{
    //    float radian = angle * Mathf.Deg2Rad;
    //    float cosAngle = Mathf.Cos(radian);
    //    float sinAngle = Mathf.Sin(radian);

    //    return new Vector3(
    //        v.x * cosAngle - v.z * sinAngle,
    //        v.y,
    //        v.x * sinAngle + v.z * cosAngle
    //    );
    //}

}
