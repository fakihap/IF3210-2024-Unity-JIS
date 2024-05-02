using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quests;
    [SerializeField]
    private List<Quest> activeQuests;
    [SerializeField]
    private bool IsInQuest() {
        return activeQuests.Count == 0;
    }
    public void Start() {
        foreach (Quest quest in quests) {
            quest.Subscribe(this);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && quests.Count > 0) {
            StartQuest(quests[0]);
        }
    }

    public void StartQuest(Quest quest) {
        quests.Remove(quest);
        activeQuests.Add(quest);

        quest.ActivateQuest();
    }

    public void FinishQuest(Quest quest) {
        activeQuests.Remove(quest);
    }
}
