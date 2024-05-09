using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField]
    public List<Quest> activeQuests; // later set this private

    private QuestUIManager questUIManager;

    [SerializeField]
    private bool IsInQuest() {
        return activeQuests.Count == 0;
    }

    public void Awake() {
        // Instance 
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);


        // quest UI
        questUIManager = FindObjectOfType<QuestUIManager>(); // later change this into singleton
        questUIManager.SetQuestList(activeQuests);
    }

    public void Update() {
        foreach(Quest quest in activeQuests) {
            quest.UpdateQuest(); // quest reserved
        }
    }

    // public void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U) && quests.Count > 0) {
    //         StartQuest(quests[0]);
    //     }


    // }

    // public void StartQuest(Quest quest) {
    //     quests.Remove(quest);
    //     activeQuests.Add(quest);

    //     quest.ActivateQuest();

    //     UpdateUI();
    // }

    public void FinishQuest(Quest quest) {
        Debug.Log("harusnya masuk sini" + quest.name);
        GameDirector.Instance.UpdateDirector();
        activeQuests.Remove(quest);

        UpdateUI();       
    }

    // only call this method on the very last
    public void UpdateUI() {
        questUIManager.UpdateUI();
    }
}
