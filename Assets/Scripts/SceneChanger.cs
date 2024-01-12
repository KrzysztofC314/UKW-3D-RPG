using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float Distance = 25f;
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private int SceneNumber;
    [SerializeField] private GameManager gameManager;

    void Update()
    {
        if (playerTransform != null) // Sprawdü, czy obiekt gracza zosta≥ przypisany
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance <= Distance) // Jeúli odleg≥oúÊ jest mniejsza lub rÛwna zadanej odleg≥oúci
            {
                isInRange = true;
                //Debug.Log("Gracz jest w zasiÍgu!"); // Informacja o byciu w zasiÍgu
            }
            else
            {
                isInRange = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) // Sprawdü, czy nastπpi≥o klikniÍcie
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject && isInRange == true)
                {
                    Logic();
                }
                if (hit.collider.gameObject == gameObject && isInRange == false)
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
        if (isWaiting == true && isInRange == true)
        {
            Logic();
        }
    }
    void Logic()
    {
        if (gameManager.isFight == false)
        {
            Debug.Log("NastÍpny lvl");
            isWaiting = false;
            //SceneManager.LoadScene(SceneNumber);
        }
    }
}
