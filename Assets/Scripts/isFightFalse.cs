using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFightFalse : MonoBehaviour
{
    public GameManager gameManager;


    void Update()
    {
        // Sprawdzenie czy przycisk O zosta� naci�ni�ty
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Wywo�anie funkcji do zmiany warto�ci isFight
            gameManager.isFight = false;
        }
    }
}
