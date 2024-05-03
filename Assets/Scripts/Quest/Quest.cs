using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[Serializable]
public abstract class Quest : MonoBehaviour
{
    [SerializeField]
    protected List<QuestNotifier> notifiers;
    [SerializeField]
    protected bool isActive = false; // might delete this later as its purpose was only to tell us whether the quest is active or not
    protected QuestManager questManager;

    protected abstract void StartQuest();
    public abstract void ProgressQuest();
    protected abstract bool CheckQuest();
    public abstract string GetQuestMessage();

    public void Subscribe(QuestManager questManager) {
        this.questManager = questManager;
    }

    public void Update() {
        if (CheckQuest() && isActive) {
            FinishQuest();
        }
    }
    
    public void ActivateQuest() {
        isActive = true;
        StartQuest();

        SubscribeToNotifiers();
        Debug.Log("Quest Started : " + GetQuestMessage());
    }

    private void FinishQuest(){
        isActive = false;

        Debug.Log("Task Finished : " + GetQuestMessage());

        questManager.FinishQuest(this);
        UnsubscribeFromNotifiers();
    }

    void SubscribeToNotifiers() {
        foreach (QuestNotifier questNotifier in notifiers) {
            questNotifier.Subscribe(this);
        }
    }

    void UnsubscribeFromNotifiers() {
        foreach (QuestNotifier questNotifier in notifiers) {
            questNotifier.Unsubscribe(this);
        }
    }
} 