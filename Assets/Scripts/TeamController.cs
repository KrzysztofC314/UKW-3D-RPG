using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public GameObject[] teamComp = new GameObject[4];
    public enum Characters
    {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3,
    }

    public Characters[] teamOrder = new Characters[4];

    public Transform[] playerPositions = new Transform[4];

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            int chosenCharacter = (int)teamOrder[i];
            Instantiate(teamComp[chosenCharacter], playerPositions[i]);
        }
    }

}
