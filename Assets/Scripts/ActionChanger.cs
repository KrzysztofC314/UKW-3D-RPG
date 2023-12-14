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
        // Dodaj obs³ugê dla ka¿dego przycisku
        for (int i = 0; i < uiActionButton.Length; i++)
        {
            int index = i; // Zapamiêtaj aktualny indeks dla delegata
            uiActionButton[i].ActionButton.onClick.AddListener(() => OnActionButtonClick(index));
        }
    }

    // Obs³uga klikniêcia przycisku
    void OnActionButtonClick(int buttonIndex)
    {
        // SprawdŸ, czy GameManager jest dostêpny
        if (gameManager != null)
        {
            // Zmieñ wartoœæ zmiennej Action w GameManagerze na wartoœæ z przycisku
            gameManager.Action = uiActionButton[buttonIndex].ActionTo;
        }
        else
        {
            Debug.LogWarning("GameManager nie jest przypisany!");
        }
    }
}
