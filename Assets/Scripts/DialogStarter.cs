using UnityEngine;
using DialogueEditor;

public class DialogStarter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distance = 5f; 
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private NPCConversation myConv;
    [SerializeField] private bool isForced = false;
    [SerializeField] private GameManager gameManager;

    void Update()
    {
        if (playerTransform != null) // Sprawdü, czy obiekt gracza zosta≥ przypisany
        {
            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

            if (playerDistance <= distance) // Jeúli odleg≥oúÊ jest mniejsza lub rÛwna zadanej odleg≥oúci
            {
                isInRange = true;
                //Debug.Log("Gracz jest w zasiÍgu");
            }
            else
            {
                isInRange = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) // Sprawdü, czy nastπpi≥o klikniÍcie
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == gameObject && isInRange)
                    {
                        Logic();
                    }
                    if (hit.collider.gameObject == gameObject && !isInRange)
                    {
                        isWaiting = true;
                    }
                    else
                    {
                        isWaiting = false;
                    }
                }
                else
                {
                    isWaiting = false; // Jeúli raycast nie trafi w øaden obiekt ustaw isWaiting na false
                }
            }
        }

        if (isWaiting && isInRange && !isForced)
        {
            Logic();
        }
    }

    void Logic()
    {
        if (gameManager.isFight == false)
        {
            Debug.Log("Conversation Start");
            isWaiting = false;
            ConversationManager.Instance.StartConversation(myConv);
        }
    }
}
