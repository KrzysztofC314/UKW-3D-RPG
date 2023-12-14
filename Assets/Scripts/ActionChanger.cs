using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionChanger : MonoBehaviour
{
    [System.Serializable]
    public struct UIActionButton
    {
        public Button ActionButton;
        public int ActionTo;
    }

    public GameManager gameManager;
    public UIActionButton[] uiActionButton;

    // Start is called before the first frame update
    void Start()
    {
        // Dodaj obs�ug� dla ka�dego przycisku
        for (int i = 0; i < uiActionButton.Length; i++)
        {
            int index = i; // Zapami�taj aktualny indeks dla delegata
            uiActionButton[i].ActionButton.onClick.AddListener(() => OnActionButtonClick(index));
        }
    }

    // Obs�uga klikni�cia przycisku
    void OnActionButtonClick(int buttonIndex)
    {
        // Sprawd�, czy GameManager jest dost�pny
        if (gameManager != null)
        {
            // Zmie� warto�� zmiennej Action w GameManagerze na warto�� z przycisku
            gameManager.Action = uiActionButton[buttonIndex].ActionTo;
        }
        else
        {
            Debug.LogWarning("GameManager nie jest przypisany!");
        }
    }
}
