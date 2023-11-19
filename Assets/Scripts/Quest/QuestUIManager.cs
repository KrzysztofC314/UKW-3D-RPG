using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;


    //BOOLS
    public bool questAvailable = false;
    public bool questRunning = false;
    private bool questPanelAcitive = false;
    private bool questLogPanelActive = false;

    //PANELS
    public GameObject questPanel;
    public GameObject questLogPanel;

    //QUESTOBJECT
    private QuestObject currentQuestObject;

    //QUESTLISTS
    public List<Quest> availableQuest = new List<Quest>();
    public List<Quest> activeQuest = new List<Quest>();

    //BUTTONS
    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;

    //SPACER
    public Transform qButtonSpacer1;//qButton available
    public Transform qButtonSpacer2;//running qButton
    public Transform qLogButtonSpacer; //running in qLog

    //QUEST INFOS
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSummary;

    //QUEST LOG INFOS
    public TextMeshProUGUI questLogTitle;
    public TextMeshProUGUI questLogDescription;
    public TextMeshProUGUI questLogSummary;

    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questPanelAcitive = !questPanelAcitive;
            //showQuestLogpanel
        }    
    }

    //CALLED FROM QUEST OBJECT
    public void CheckQuests(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);
        if ((questRunning || questAvailable) && !questPanelAcitive)
        {
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No Quest Available");
        }
    }

    //SHOW PANEL
    public void ShowQuestPanel()
    {
        questPanelAcitive = true;
        questPanel.SetActive(questPanelAcitive);
        //FILL IN DATA
        FillQuestButtons();
    }

    // quest Log

    //HIDE QUEST PANEL
    public void HideQuestPanel()
    {
        questPanelAcitive = false;
        questAvailable = false;
        questRunning = false;

        //Clear TEXT
        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        //CLEAR LISTS
        availableQuest.Clear();
        activeQuest.Clear();
        //CLEAR BUTTON LIST
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        //Hide panel
        questPanel.SetActive(questPanelAcitive);
    }


    // FILL BUTTONS FOR QUEST PANEL
    void FillQuestButtons()
    {
        foreach (Quest availableQuest in availableQuest)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = availableQuest.id;
            qBScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQuest in activeQuest)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = activeQuest.id;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);
        }
    }

}
