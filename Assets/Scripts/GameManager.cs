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
    public int Turn = 1;

    public KeyCode PauseButton = KeyCode.Space;

    public Button Player1;
    public Button Player2;
    public Button Player3;
    public Button Player4;

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
            isPause = !isPause; // Zmiana warto�ci boola isPause na przeciwn� warto�� po wci�ni�ciu klawisza
        }
    }

}
