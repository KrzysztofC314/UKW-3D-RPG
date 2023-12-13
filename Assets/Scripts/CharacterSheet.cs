using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    public GameManager gameManager;

    public int MaxHealth = 10;
    public int Health = 10;
    public int MaxEnergy = 10;
    public int Energy = 10;

    public int Initiative = 10;
    public int Allience = 1;
    public int PlayerTurn = 1;

    public int Level = 1;

    public int Strength = 10;
    public int Dexterity = 10;
    public int Intellect = 10;
    public int Charisma = 10;

    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text EP;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "HP: " + Health + "/" + MaxHealth;
        EP.text = "EP: " + Energy + "/" + MaxEnergy;
    }
}
