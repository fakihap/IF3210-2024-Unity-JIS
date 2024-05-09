using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[Serializable]
public abstract class Quest : DirectableObject
{
    [SerializeField]
    protected bool isActive = false; // might delete this later as its purpose was only to tell us whether the quest is active or not
    [SerializeField]
    protected bool isCompleted = false;
    
# region Quest
    protected abstract void StartQuest();
    public abstract void ProgressQuest();
    public abstract string GetQuestMessage();

    public virtual void UpdateQuest() {
        if (IsCompleted()) {
            return;
        }
    }

    // public void Update() {
    //     UpdateQuest();

        // finish is called when quest is completed but still active
        // now checked by GameDirector
        // if (IsActive() && IsCompleted()) {
        //     FinishQuest();
        // }
    // }
    
    private void ActivateQuest() {
        isActive = true;
        StartQuest();

        // SubscribeToNotifiers();
        Debug.Log("Quest Started : " + GetQuestMessage());
    }

    private void FinishQuest(){
        isActive = false;

        Debug.Log("Task Finished : " + GetQuestMessage());

        QuestManager.Instance.FinishQuest(this);
        // UnsubscribeFromNotifiers();
    }
# endregion
    
# region DirectableObject
    public override bool IsActive()
    {
        return isActive;
    }
    public override bool IsCompleted()
    {
        return isCompleted;
    }    
    protected override void StartDirectable() {
        Debug.LogWarning("STATING A QUEST");

        // needs to recheck this
        // we refactor the scheme of subscribing pattern
        ActivateQuest();

        // to update quest UI
        QuestManager.Instance.activeQuests.Add(this); // later need to change this
        QuestManager.Instance.UpdateUI();
    }

    protected override void EndDirectable()
    {
        Debug.Log("end dirre");
        FinishQuest();
        QuestManager.Instance.UpdateUI();
    }

    public override void ResetDirectable() {
        isActive = false;
        isCompleted = false;
    }
    #endregion




    // skema nya diganti
    // void SubscribeToNotifiers() {
    //     foreach (QuestNotifier questNotifier in notifiers) {
    //         questNotifier.Subscribe(this);
    //     }
    // }

    // void UnsubscribeFromNotifiers() {
    //     foreach (QuestNotifier questNotifier in notifiers) {
    //         questNotifier.Unsubscribe(this);
    //     }
    // }
} 