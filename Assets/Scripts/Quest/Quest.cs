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
    protected bool isActive = false;
    private QuestManager questManager;

    protected abstract void StartQuest();
    public abstract void ProgressQuest();
    protected abstract bool CheckQuest();
    public abstract string GetQuestMessage();

    public void Start() {
        foreach (QuestNotifier questNotifier in notifiers) {
            questNotifier.Subscribe(this);
        }
    }

    public void Subscribe(QuestManager questManager) {
        this.questManager = questManager;
    }

    public void Update() {
        if (CheckQuest()) {
            FinishQuest();
        }
    }
    
    public void ActivateQuest() {
        isActive = true;
        StartQuest();

        Debug.Log("Quest Started : " + GetQuestMessage());
    }

    private void FinishQuest(){
        isActive = false;

        Debug.Log("Task Finished");

        questManager.FinishQuest(this);
    }
} 