using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public struct UIElement
    {
        public GameObject window;   
        public Button openButton;  
        public Button closeButton;
        public KeyCode menuActivationKey;
        public KeyCode menuDeactivationKey;
    }

    public GameManager gameManager;
    public UIElement[] uiElements; // tablica UI

    private void Start()
    {
        //if (gameManager.isDialog == false)
            for (int i = 0; i < uiElements.Length; i++)
            {
                int index = i; //Przechowujemy index, aby nie straciæ go w lambdach

                uiElements[i].openButton.onClick.AddListener(() =>
                {
                    OpenWindow(index);
                });

                uiElements[i].closeButton.onClick.AddListener(() =>
                {
                    CloseWindow(index);
                });

                // na start wy³¹czamy wszystkie okna
                uiElements[i].window.SetActive(false);
            }
    }

    private void Update()
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (Input.GetKeyDown(uiElements[i].menuActivationKey))
            {
                OpenWindow(i);
            }
            else if (Input.GetKeyDown(uiElements[i].menuDeactivationKey))
            {
                CloseWindow(i);
            }
        }
    }


    public void OpenWindow(int indexToOpen)
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (gameManager.isDialog == false)
            {
                if (i == indexToOpen)
                {
                    uiElements[i].window.SetActive(true);
                    gameManager.isMenu = true;
                }

                else
                {
                    uiElements[i].window.SetActive(false);
                }
            }
        }
    }

    public void CloseWindow(int indexToClose)
    {
        uiElements[indexToClose].window.SetActive(false);
        gameManager.isMenu = false;
    }
}
