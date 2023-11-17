using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;

    public List<Quest> questList = new List<Quest>(); //Master Quest List
    public List<Quest> currentQuestList = new List<Quest>(); //Current Quest List

    public bool quest1 = false;

    //private vars for our QuestObject

    private void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject NPCQuestObject)
    {
        if (NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        Debug.Log("Quest ID: " + NPCQuestObject.availableQuestIDs[j] + " " + questList[i].progress);

                        //Testing
                        AcceptQuest(NPCQuestObject.availableQuestIDs[j]);
                        // quest ui manager
                    }
                }
            }
        }

        //Active Quest
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; i < NPCQuestObject.receivableQuestIDs.Count; i++)
            {
                if (currentQuestList[i].id == NPCQuestObject.receivableQuestIDs[j] && currentQuestList[i].progress == Quest.QuestProgress.ACCEPT || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    Debug.Log("Quest ID: " + NPCQuestObject.receivableQuestIDs[j] + " is " + currentQuestList[i].progress);

                    CompleteQuest(NPCQuestObject.receivableQuestIDs[j]);
                    // quest ui manager
                }
            }
        }

    }

    //akceptowanie questów

    public void AcceptQuest(int questID)
    {
        for(int i =0; i<questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPT;
            }
        }
    }

    //podawanie questów

    public void GiveUpQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.ACCEPT)
            {
                currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }

    }

            //ukanczanie questow
    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);

                // Reward
            }

        }
        // Check for chain quests
        CheckChainQuest(questID);
    }

    //Check Chain Quest
    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if (tempID > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }

    //Dodawanie przedmiotow

    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPT)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }

            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestProgress.ACCEPT)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }

    //Usuwanie przedmiotow

    //BOOL

    public bool RequestAvailableQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPT)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }

    //BOOLS 2 Electrick Bugalu

    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
                
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPT)
                {
                    return true;
                }
            }

        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }

        }
        return false;
    }
}
