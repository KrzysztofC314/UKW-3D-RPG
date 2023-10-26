using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    [SerializeField]
    private Statistics playerStats;
    private int[] statCollect;
    private int playerStrength, playerAgility, playerDexterity, playerDetermination, playerIntelect, playerPerception;

    private void Start()
    {
        statCollect = playerStats.StatGather();
        playerStrength = statCollect[0];
        playerAgility = statCollect[1];
        playerDexterity = statCollect[2];
        playerDetermination = statCollect[3];
        playerIntelect = statCollect[4];
        playerPerception = statCollect[5];

    }
}
