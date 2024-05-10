using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private List<Quest> activeQuests;
    [SerializeField] private TextMeshProUGUI questText;

    public void SetQuestList(List<Quest> quests) {
        activeQuests = quests;
    }

    void OnEnable() {
        // QuestManager.Instance.Subscribe(this);
    }

    public void Start() {
        QuestManager.Instance.Subscribe(this);
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

    void OnDisable() {
        // QuestManager.Instance.Unsubscribe(this);
    }
}
