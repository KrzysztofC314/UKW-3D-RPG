using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Attack : MonoBehaviour
{
    [SerializeField] private int attackRange = 25; // Zasiêg ataku
    [SerializeField] private int damage = 5;

    [SerializeField] GameManager gameManager;
    [SerializeField] private CharacterSheet characterSheet;

    [SerializeField] private TMP_Text AttackDistance;

    private float _distance; // Prywatna zmienna przechowuj¹ca odleg³oœæ

    // Update is called once per frame
    void Update()
    {
        if (!IsPointerOverUI() && gameManager.Turn == characterSheet.PlayerTurn && gameManager.isFight == true && gameManager.Action == 2)
        {
            // Raycast
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Oblicz odleg³oœæ i przypisz do zmiennej prywatnej
                _distance = Vector3.Distance(transform.position, hit.point);
                AttackDistance.text = "AttackDistance: " + _distance.ToString("F2");

                // SprawdŸ czy odleg³oœæ jest wiêksza ni¿ attackRange
                if (_distance > attackRange)
                {
                    AttackDistance.color = Color.red; // Ustawienie koloru tekstu na czerwony, jeœli odleg³oœæ jest wiêksza ni¿ zasiêg ataku
                }
                else
                {
                    AttackDistance.color = new Color(0.53f, 0.81f, 0.98f); // Ustawienie jaœniejszego niebieskiego koloru tekstu
                }

                if (hit.collider.CompareTag("Enemy") && Input.GetMouseButtonDown(0) && !IsPointerOverUI() && characterSheet.FightAction > 0)
                {
                    // Obróæ postaæ w stronê wroga
                    Vector3 direction = hit.transform.position - transform.position;
                    direction.y = 0f; // Blokujemy obrót w osi Y, jeœli postaæ jest p³aska na ziemiê
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                    // Zadaj obra¿enia wrogowi
                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        characterSheet.FightAction--; // Zmniejszamy liczbê dostêpnych akcji walki o 1
                        Debug.Log("Trafiony!");
                    }
                }
            }
        }
    }

    private bool IsPointerOverUI() //sprawdza czy myszka jest na ui
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
