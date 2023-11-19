using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{

    public enum QuestProgress {NOT_AVAILABLE, AVAILABLE, ACCEPT, COMPLETE, DONE}

    public string title; //nazwa questu
    public int id; //numer id do questu
    public QuestProgress progress; //status danego questa
    public string description; // String dla naszego questa od givera/recivera
    public string hint; // String dla naszego questa od givera/recivera
    public string congratulation; // String dla naszego questa od givera/recivera
    public string summary; // String dla naszego questa od givera/recivera
    public int nextQuest; // Nastepny quest jezeli taki jest

    public string questObjective; // nazwa questu objectivu 
    public int questObjectiveCount; // liczba aktywnych teraz questow
    public int questObjectiveRequirement; //wymagana liczba objectivow w questach

    public int expReward;
    public int goldReward;
    public string itemReward;
}
