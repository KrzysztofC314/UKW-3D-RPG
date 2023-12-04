using UnityEngine;
using UnityEngine.UI;

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

    public KeyCode PauseButton = KeyCode.Space;

    public Button Player1;
    public Button Player2;
    public Button Player3;
    public Button Player4;

    //---------------------------------------

    public int Health = 10;
    public int Energy = 10;

    public int Class = 1;

    public int Relation = 10;

    public int Strength = 10;
    public int Dexterity = 10;
    public int Intellect = 10;
    public int Charisma = 10;

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

    private void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            isPause = !isPause; // Zmiana wartoœci boola isPause na przeciwn¹ wartoœæ po wciœniêciu klawisza
        }
    }

}
