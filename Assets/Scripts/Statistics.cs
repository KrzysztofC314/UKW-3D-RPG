using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatistics", menuName = "ScriptableObjects/Statistics")]
public class Statistics : ScriptableObject
{
    public enum Sex
    {
        M,
        F,
        A,
    }
    public string characterName = "Bezimienny";

    [SerializeField]
    private Sex selectedSex = Sex.M;

    [SerializeField]
    private int currentHealthPoints = 10;
    [SerializeField]
    private int maxHealthPoints = 10;

    [SerializeField]
    private int luckyPoints = 10;
    [SerializeField]
    private int pp = 0;

    [SerializeField]
    private int lvl = 0;
    private int lvlThreshold = 1000;
    [SerializeField]
    private int exp = 0;
    private int expToNextLvl;

    [SerializeField]
    private int age = 25;

    [SerializeField]
    private int strength = 10;
    [SerializeField]
    private int agility = 10;
    [SerializeField]
    private int dexterity = 10;
    [SerializeField]
    private int determination = 10;
    [SerializeField]
    private int intellect = 10;
    [SerializeField]
    private int perception = 10;

    private int LvlUpModify = 20;
    private int PPModify = 20;

    public void Damage(int damagePoints)
    {
        currentHealthPoints -= damagePoints;
        Debug.Log(currentHealthPoints);
        if (currentHealthPoints <= 0)
        {
            Debug.Log("Dead");
            currentHealthPoints = 0;
        }
    }

    public void Heal(int healPoints)
    {
        if (currentHealthPoints < maxHealthPoints)
        {
            currentHealthPoints += healPoints;
            Debug.Log(currentHealthPoints);
        }
        else
        {
            Debug.Log("Max Health");
        }
    }

    public void ExpUp(int expGain)
    {
        exp += expGain;
        Debug.Log(exp);
        if (exp >= lvlThreshold)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (exp >= lvlThreshold)
        {
            lvl++;
            exp -= lvlThreshold;
            Debug.Log("Level " + lvl);
        }
        else
        {
            Debug.Log("Insufficient Experience");
        }
    }

    public int SexCheck()
    {
        int sexNumber = 0;
        switch (selectedSex)
        {
            case Sex.M:
                sexNumber = 1;
                break;
            case Sex.F:
                sexNumber = 2;
                break;
            case Sex.A:
                sexNumber = 3;
                break;
        }
        return sexNumber;
    }

    public int[] StatGather()
    {
        int[] statChart = {strength, agility, dexterity, determination, intellect, perception };

        return statChart;
    }



}
