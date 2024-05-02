using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quests;
    [SerializeField]
    private int currentIndex = -1;

    [SerializeField]
    private bool isInQuest = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuest() {
        currentIndex += 1;
        isInQuest = true;

        quests[currentIndex].ActivateQuest();
    }
}
