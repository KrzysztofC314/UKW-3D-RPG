using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFightFalse : MonoBehaviour
{
    public GameManager gameManager;


    void Update()
    {
        // Sprawdzenie czy przycisk O zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Wywo³anie funkcji do zmiany wartoœci isFight
            gameManager.isFight = false;
        }
    }
}
