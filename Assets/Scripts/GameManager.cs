using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class GameManager : MonoBehaviour
{
    public bool isMenu = false;
    public bool isDialog = false;
    public int Language = 0;
    public int ActivePlayer = 1;
    public bool isTeam = true;
    public bool isPause = false;
    public bool isFight = false;
    public int Action = 1;
    public int Turn = 1;
    private int botAmount = 0;

    public int CurrentDialog = 0;

    public KeyCode PauseButton = KeyCode.Space;

    public Button Player1;
    public Button Player2;
    public Button Player3;
    public Button Player4;

    [SerializeField] private Button NextTurn;
    private TurnManager turnManager;

    //---------------------------------------
    /*
    public int MaxHealth;
    public int Health;
    public int MaxEnergy;
    public int Energy;

    public int Strength;
    public int Dexterity;
    public int Intellect;
    public int Charisma;
    */

    void Start()
    {
        turnManager = GetComponent<TurnManager>();
        if (NextTurn != null)
        {
            NextTurn.onClick.AddListener(OnClickNextTurn);
        }
    }
    private void Update()
    {


        if (Turn > turnManager.turnAmount)
        {
            Turn = 1;
        }
    }

    public void ONisDialog()
    {
        isDialog = true;
    }
    public void OFFisDialog()
    {
        isDialog = false;
    }
    public void ONisFight()
    {
        isFight = true;
    }
    public void OFFisFight()
    {
        isFight = false;
    }


    //Skrypt do zmieniania active playera w grze
    public void SetActivePlayerTo(int player)
    {
        ActivePlayer = player;
    }

    //Skrypt do zmieniania boola isTeam w grze
    public void SwitchTeamstate()
    {
        isTeam = !isTeam;
    }

    public void OnClickNextTurn()
    {
        botAmount = 0;
        Turn++;

        turnManager.SortByInitiative();
        turnManager.IsBot();
        for (int i = 0; i < turnManager.isBot.Length; i++)
        {
            if (turnManager.isBot[i] == true)
            {
                botAmount++;
            }
        }

        if (botAmount <= 0)
        {
            isFight = false;
        }

    }
}
