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

    public Button Player1;
    public Button Player2;
    public Button Player3;
    public Button Player4;

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

   
}
