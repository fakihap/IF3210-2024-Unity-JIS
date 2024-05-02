using UnityEngine;
using System.Collections.Generic;
public enum QuestType {
    Travel,
    Defeat
}
public abstract class Quest : MonoBehaviour
{
    [SerializeField]
    protected QuestType questType;
    [SerializeField]
    protected List<QuestNotifier> notifiers;
    [SerializeField]
    protected bool isActive = false;

    protected abstract void StartQuest();
    public abstract void ProgressQuest();
    protected abstract bool CheckQuest();
    public abstract string GetQuestMessage();

    void Start() {
        foreach (QuestNotifier questNotifier in notifiers) {
            questNotifier.Subscribe(this);
        }
    }

    void Update() {
        if (CheckQuest()) {
            FinishQuest();
        }
    }
    
    public void ActivateQuest() {
        isActive = true;
        StartQuest();
    }

    private void FinishQuest(){
        isActive = false;

        Debug.Log("Task Finished");
    }
} 