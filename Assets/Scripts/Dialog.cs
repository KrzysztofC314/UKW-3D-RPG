using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private string playerTag = "Player";  // Tag gracza
    [SerializeField] private float activationDistance = 3.0f;  // Minimalna odleg�o�� do aktywacji dialogu
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FlagManager FlagManager;

    [SerializeField] private QuestManager quest; // pierwsza opcja dialogowa po klikni�ciu aktywuje quest1 = true

    [SerializeField] private float DialogDelay = 0;
    [SerializeField] private int DialogStage = 0;
    [SerializeField] private bool dialogActivated = false;
    [SerializeField] private TeamController teamController;

    [SerializeField] private int DialogNumber;

    private bool D1 = true;
    private bool D2 = true;
    private bool D3 = true;

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
        if (Input.GetMouseButtonDown(1) && !dialogActivated) // Sprawd�, czy dialog nie zosta� wcze�niej aktywowany
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
                    DialogStage += 1; // Zwi�ksz DialogStage o 1
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




    // Dodaj metod� do dezaktywowania dialogu
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
        teamController.isDialogue = dialogActivated;
        if (DialogDelay > 0f) // Odejmowanie 1 od DialogDelay co sekund�
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

        if (gameManager.CurrentDialog == DialogNumber)
        {
            // wywo�ywanie private void
            if (gameManager.Language == 0 && gameManager.isFight == false)
            {
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

            if (gameManager.Language == 1 && gameManager.isFight == false)
            {
                if (DialogStage == 1)
                {
                    Stage01();
                }
                if (DialogStage == 2)
                {
                    Stage02();
                }
                if (DialogStage == 3)
                {
                    Stage03();
                }
                if (DialogStage == 4)
                {
                    Stage04();
                }
                if (DialogStage == 5)
                {
                    Stage05();
                }
            }
        }
    }

    public void Stage1()
    {
            dialogText.gameObject.SetActive(true);
            dialogText.text = "Witaj podr�niku, odpowiedz mi na pytanie. Co by�o pierwsze, jajko czy kura?";
        // audioSource.clip = audioClips[0];
        // audioSource.Play();
        // DialogDelay = 3.5f;

        if (FlagManager.Flag == 0)
        {
            answerButtons[0].gameObject.SetActive(true);
            answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Kura";
        }
       
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
            D1 = false;
            quest.quest1 = true;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 3;
            D2 = false;
        });
        answerButtons[2].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 4;
            D3 = false;
        });
        answerButtons[3].onClick.AddListener(() =>
        {
            Deactivate();
        });

    }

    public void Stage2()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Nie, to nie by�a kura";
        
        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo�esz powt�rzy� pytanie?";

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
        dialogText.text = "Nie, to nie by�o jajko";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo�esz powt�rzy� pytanie?";

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
        dialogText.text = "Bardzo mo�liwe �e by� to pterodaktyl";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Czy mo�esz powt�rzy� pytanie?";

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
        dialogText.text = "Co by�o pierwsze, jajko czy kura?";

        if (D1 == true)
        {
            answerButtons[0].gameObject.SetActive(true);
            answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Kura";
        }

        if (D2 == true)
        {
            answerButtons[1].gameObject.SetActive(true);
            answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Jajko";
        }

        if (D3 == true)
        {
            answerButtons[2].gameObject.SetActive(true);
            answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Pterodaktyl";
        }

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






    public void Stage01()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Hello traveler, answer my question. Which came first, the egg or the chicken?";
        // audioSource.clip = audioClips[0];
        // audioSource.Play();
        // DialogDelay = 3.5f;

        
        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Chicken";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Egg";

        answerButtons[2].gameObject.SetActive(true);
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Pterodactyl";

        answerButtons[3].gameObject.SetActive(true);
        answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = "I don't know [END]";

        // if (DialogDelay == 0)
        // {
        //     DialogStage = 2;
        // }

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 2;
            D1 = false;
            quest.quest1 = true;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 3;
            D2 = false;
        });
        answerButtons[2].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 4;
            D3 = false;
        });
        answerButtons[3].onClick.AddListener(() =>
        {
            Deactivate();
        });

    }

    public void Stage02()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "No, it wasn't a chicken";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Can you repeat the question?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[END]";

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

    public void Stage03()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "No, it wasn't an egg";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Can you repeat the question?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[END]";

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

    public void Stage04()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "It is very possible that it was a pterodactyl";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Can you repeat the question?";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "[END]";

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

    public void Stage05()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Which came first, the egg or the chicken?";

        if (D1 == true)
        {
            answerButtons[0].gameObject.SetActive(true);
            answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Chicken";
        }

        if (D2 == true)
        {
            answerButtons[1].gameObject.SetActive(true);
            answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Egg";
        }

        if (D3 == true)
        {
            answerButtons[2].gameObject.SetActive(true);
            answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Pterodactyl";
        }

        answerButtons[3].gameObject.SetActive(true);
        answerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = "I don't know [END]";

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