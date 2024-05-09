using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded: " + scene.name);
        
        // quest UI
        // idk wheteher to set it singleton or additive load + refer on load
        questUIManager = FindObjectOfType<QuestUIManager>(); // later change this into singleton
        if (questUIManager != null) {
            questUIManager.SetQuestList(activeQuests);   
        }
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        GameDirector.Instance.UpdateDirector();
        activeQuests.Remove(quest);

        UpdateUI();       
    }

    // only call this method on the very last
    public void UpdateUI() {
        questUIManager.UpdateUI();
    }
}
