using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private string playerTag = "Player";  // Tag gracza
    public float activationDistance = 3.0f;  // Minimalna odleg³oœæ do aktywacji dialogu
    public TMP_Text dialogText;
    public Button[] answerButtons;
    public GameManager gameManager;

    public float DialogDelay = 0;
    public int DialogStage = 0;
    public bool dialogActivated = false;

    //public AudioSource audioSource;
   // public AudioClip[] audioClips; 

    private float countdownTimer = 1f; // do timera

    void Start()
    {
        // Dezaktywuj 
        dialogText.gameObject.SetActive(false);

        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !dialogActivated) // SprawdŸ, czy dialog nie zosta³ wczeœniej aktywowany
        {
            ActivateDialog();
        }
    }

    private void ActivateDialog()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= activationDistance)
            {
                // Aktywuj isDialog w skrypcie GameManager
                if (gameManager != null)
                {
                    gameManager.isDialog = true;
                    DialogStage += 1; // Zwiêksz DialogStage o 1
                    dialogActivated = true; // Oznacz dialog jako aktywowany
                }
            }
        }
    }

    public void Clean()
    {
        dialogText.gameObject.SetActive(false);
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }




    // Dodaj metodê do dezaktywowania dialogu
    public void Deactivate()
    {
        if (gameManager != null)
        {
            gameManager.isDialog = false;
            DialogStage = 0;
            dialogActivated = false;

            dialogText.gameObject.SetActive(false);

            foreach (Button button in answerButtons)
            {
                button.gameObject.SetActive(false);
            }

        }
    }
    void Update()
    {
        if (DialogDelay > 0f) // Odejmowanie 1 od DialogDelay co sekundê
        {
            countdownTimer -= Time.deltaTime; 
            if (countdownTimer <= 0f)
            {
                DialogDelay -= 1f; 
                countdownTimer = 1f; 
            }

            if (DialogDelay < 0f)
            {
                DialogDelay = 0f; 
            }
        }

        // wywo³ywanie private void
        if (DialogStage == 1) 
        {
            Stage1();
        }
        if (DialogStage == 2)
        {
            Stage2();
        }
        if (DialogStage == 3)
        {
            Stage3();
        }
        if (DialogStage == 4)
        {
            Stage4();
        }
        if (DialogStage == 5)
        {
            Stage5();
        }
    }

    public void Stage1()
    {
            dialogText.gameObject.SetActive(true);
            dialogText.text = "Witaj podró¿niku, odpowiedz mi na pytanie. Co by³o pierwsze, jajko czy kura?";
           // audioSource.clip = audioClips[0];
           // audioSource.Play();
           // DialogDelay = 3.5f;

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Kura";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Jajko";

        answerButtons[2].gameObject.SetActive(true);
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Pterodaktyl";

        answerButtons[3].gameObject.SetActive(true);
        answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = "Nie wiem [KONIEC]";

        // if (DialogDelay == 0)
        // {
        //     DialogStage = 2;
        // }

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 2;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 3;
        });
        answerButtons[2].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 4;
        });
        answerButtons[3].onClick.AddListener(() =>
        {
            Deactivate();
        });

    }

    public void Stage2()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Nie, to nie by³a kura";
        
        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo¿esz powtórzyæ pytanie?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[KONIEC]";

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 5;
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }

    public void Stage3()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Nie, to nie by³o jajko";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo¿esz powtórzyæ pytanie?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[KONIEC]";

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 5;
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }

    public void Stage4()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Bardzo mo¿liwe ¿e by³ to pterodaktyl";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo¿esz powtórzyæ pytanie?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[KONIEC]";

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 5;
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }

    public void Stage5()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Co by³o pierwsze, jajko czy kura?";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Kura";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Jajko";

        answerButtons[2].gameObject.SetActive(true);
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Pterodaktyl";

        answerButtons[3].gameObject.SetActive(true);
        answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = "Nie wiem [KONIEC]";

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 2;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 3;
        });
        answerButtons[2].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 4;
        });
        answerButtons[3].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }
}