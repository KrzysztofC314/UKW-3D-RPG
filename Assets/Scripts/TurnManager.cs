using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public UnityEvent startFight;
    public UnityEvent NextTurn;
    private GameManager gameManager;
    private CharacterSheet[] characterSheets;
    [HideInInspector] public bool[] isBot;
    public int turnAmount;

    

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameManager.isFight)
        {
            SortByInitiative();
            IsBot();
            startFight.Invoke();
        }
    }

    public void SortByInitiative()
    {
        characterSheets = FindObjectsByType<CharacterSheet>(FindObjectsSortMode.None);
        Array.Sort(characterSheets, (a, b) => b.Initiative.CompareTo(a.Initiative));
        turnAmount = characterSheets.Length;
        for(int i = 0; i < characterSheets.Length; i++)
        {
            characterSheets[i].PlayerTurn = i + 1;
        }

    }

    public void IsBot()
    {
        for (int i = 0; i < characterSheets.Length; i++)
        {
            isBot[i] = characterSheets[i].isBot;
        }

    }


}