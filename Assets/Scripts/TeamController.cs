using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    private int teamStrength, teamAgility, teamDexterity, teamDetermination, teamIntellect, teamPerception;
    private PlayerManager mainPlayerManager;
    public GameObject[] teamComp = new GameObject[4];
    public enum Characters
    {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3,
    }

    public enum Positions
    {
        Position1 = 0,
        Position2 = 1,
        Position3 = 2,
        Position4 = 3,
    }

    public Characters[] teamOrder = new Characters[4];

    public Transform[] playerPositions = new Transform[4];

    public Positions chosenCharacter;

    private void Start()
    {
        SpawnPlayers();
        GetData();
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            int selectedCharacter = (int)teamOrder[i];
            Instantiate(teamComp[selectedCharacter], playerPositions[i]);
        }
    }

    private void GetData()
    {
        int selectedCharacter = (int)chosenCharacter;
        mainPlayerManager = teamComp[selectedCharacter].GetComponent<PlayerManager>();
        Statistics mainPlayerStats = mainPlayerManager.playerStats;
        int[] statCollect;
        statCollect = mainPlayerStats.StatGather();
        teamStrength = statCollect[0];
        teamAgility = statCollect[1];
        teamDexterity = statCollect[2];
        teamDetermination = statCollect[3];
        teamIntellect = statCollect[4];
        teamPerception = statCollect[5];
    }

}
