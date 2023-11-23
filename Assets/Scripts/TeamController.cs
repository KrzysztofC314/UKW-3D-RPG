using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    private int teamStrength, teamAgility, teamDexterity, teamDetermination, teamIntellect, teamPerception;
    private PlayerManager mainPlayerManager;
    private Movement chosenMovement;
//Array prefabów postaci
    public GameObject[] teamComp = new GameObject[4];
    [HideInInspector] public bool isDialogue;
    [HideInInspector] public Vector3 movementTrack;
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
//Array maj¹cy decydowaæ jakie postacie maj¹ siê spawnowaæ
    public Characters[] teamOrder = new Characters[4];
//Array pustych gameobjectów w których postacie siê maj¹ spawnowaæ
    public GameObject[] playerPositions = new GameObject[4];

    public Positions chosenCharacter;

    private void Start()
    {
        SpawnPlayers();
        GetData();
    }

    private void Update()
    {

    }
//Instancjowanie prefabów postaci w wyznaczonych im miejscach u¿ywaj¹c transformów z arraya playerPositions
    private void SpawnPlayers()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            int selectedCharacter = (int)teamOrder[i];
            Instantiate(teamComp[selectedCharacter], playerPositions[i].transform);
        }
    }
//Zbieranie statystyk z postaci zaznaczonej jako chosen character
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
