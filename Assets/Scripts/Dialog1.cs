using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog1 : MonoBehaviour
{
    private string playerTag = "Player";  // Tag gracza
    public float activationDistance = 3.0f;  // Minimalna odleg³oœæ do aktywacji dialogu
    public TMP_Text dialogText;
    public Button[] answerButtons;
    public GameManager gameManager;
    public FlagManager FlagManager;

    public QuestManager quest; // pierwsza opcja dialogowa po klikniêciu aktywuje quest1 = true

    public float DialogDelay = 0;
    public int DialogStage = 0;
    public bool dialogActivated = false;
    [SerializeField] private TeamController teamController;

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
        teamController.isDialogue = dialogActivated;
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
        if (gameManager.Language == 0)
        {
            if (DialogStage == 1)
            {
                Stage1();
            }
            if (DialogStage == 2)
            {
                Stage2();
            }
        }

        if (gameManager.Language == 1)
        {
            if (DialogStage == 1)
            {
                Stage01();
            }
            if (DialogStage == 2)
            {
                Stage02();
            }
        }
    }

    public void Stage1()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Hej, co ty tu robisz?";
        // audioSource.clip = audioClips[0];
        // audioSource.Play();
        // DialogDelay = 3.5f;

        //if (FlagManager.Flag == 0)
        //{
        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Pracujê tu";
        //}

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Nie twój interes [WALKA]";


        // if (DialogDelay == 0)
        // {
        //     DialogStage = 2;
        // }

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 2;
            D1 = false;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            gameManager.isFight = true;
            Deactivate();
        });

    }

    public void Stage2()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Nie pamiêtam cie, ale przejdŸ";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Ha, tu cie mam [WALKA]";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Dziêki i mi³ego dnia [KONIEC]";

        answerButtons[0].onClick.AddListener(() =>
        {
            gameManager.isFight = true;
            Deactivate();
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }


    public void Stage01()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "Hey, what are you doing here?";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "I'm waiting here";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "None of your business [FIGHT]";

        answerButtons[0].onClick.AddListener(() =>
        {
            Clean();
            DialogStage = 2;
            D1 = false;
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            gameManager.isFight = true;
            Deactivate();
        });
    }

    public void Stage02()
    {
        dialogText.gameObject.SetActive(true);
        dialogText.text = "I don't remember you, but go ahead";

        answerButtons[0].gameObject.SetActive(true);
        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Ah, got you here [FIGHT]";

        answerButtons[1].gameObject.SetActive(true);
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Thanks, have a nice day [END]";

        answerButtons[0].onClick.AddListener(() =>
        {
            gameManager.isFight = true;
            Deactivate();
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            Deactivate();
        });
    }


}