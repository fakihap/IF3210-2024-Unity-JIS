using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private List<Quest> activeQuests;
    [SerializeField] private TextMeshProUGUI questText;

    // public void AddQuest(Quest quest) {
    //     activeQuests.Add(quest);

    //     UpdateUI();
    // }
    // public void UpdateQuest(Quest quest) {
    //     UpdateUI();
    // }

    // public void FinishQuest(Quest quest) {
    //     activeQuests.Remove(quest);

    //     UpdateUI();
    // }
    public void SetQuestList(List<Quest> quests) {
        activeQuests = quests;
    }

    public void Start() {
        questText = GetComponent<TextMeshProUGUI>();

        UpdateDisplay();
    }

    public void UpdateUI() {
        UpdateDisplay();
    }

    void UpdateDisplay() {
        if (activeQuests.Count == 0) {
            questText.text = "No active quests, press [U]";
            return;
        }

        string text = "";

        foreach (Quest quest in activeQuests) {
            text += string.Format("{0} -\n", quest.GetQuestMessage());
        }

        questText.text = text;
    }
}
