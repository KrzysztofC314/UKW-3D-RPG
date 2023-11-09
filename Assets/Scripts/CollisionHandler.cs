using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public string sceneToLoad;//Scene to Load
    public string spawnPointName;
    //public GameObject spawnPoint;
    //GameState


    private void OnTriggerEnter(Collider other)
    {
        QuestManager.questManager.AddQuestItem("Leave Town 1", 1);
    }

}
