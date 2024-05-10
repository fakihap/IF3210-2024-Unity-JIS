using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField]
    public List<Quest> activeQuests; // later set this private

    private List<QuestUIManager> questUIManagers = new List<QuestUIManager>();

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
       
       // now re-moved into subscriber pattern on the very bottom part of this script
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start() {
        
    }

    void Update() {
        foreach(Quest quest in activeQuests) {
            quest.UpdateQuest(); // quest reserved
        }
    }

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

# region QuestUIManager
    // realistically should only have one subscriber
    public void Subscribe(QuestUIManager questUIManager) {
        // but subscriber-notifier pattern is used in favor of 
        // its support for multiple UI instance when we just load a new scene
        // though maybe less efficient performance-wise
        questUIManager.SetQuestList(activeQuests); 
        Debug.Log(questUIManager);
        Debug.Log(questUIManagers);
        questUIManagers.Add(questUIManager);
    }

    public void Unsubscribe(QuestUIManager questUIManager) {
        questUIManagers.Remove(questUIManager);
    }

    // only call this method on the very last
    public void UpdateUI() {
        foreach (QuestUIManager questUIManager in questUIManagers) {
            questUIManager.UpdateUI();
        } 
    }

# endregion
}
