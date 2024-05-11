using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[Serializable]
public abstract class Quest : DirectableObject
{
    protected bool isActive = false; // might delete this later as its purpose was only to tell us whether the quest is active or not
    protected bool isCompleted = false;
    
# region Quest
    protected abstract void StartQuest();
    public virtual bool ProgressQuest() {
        if (!IsActive()) {
            return false;
        }

        return true;
    }
    public abstract string GetQuestMessage();

    public virtual bool UpdateQuest() {
        if (Input.GetKeyDown(KeyCode.Y)) { // cehat code, delete this later
            ProgressQuest();
        }

        if (IsCompleted()) {
            return false;
        }

        return true;
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

        Debug.Log("Quest Finished : " + GetQuestMessage());

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
        // Debug.LogWarning("STATING A QUEST");

        // needs to recheck this
        // we refactor the scheme of subscribing pattern
        ActivateQuest();

        // to update quest UI
        QuestManager.Instance.activeQuests.Add(this); // later need to change this
        QuestManager.Instance.UpdateUI();
    }

    protected override void EndDirectable()
    {
        FinishQuest();
        QuestManager.Instance.UpdateUI();
    }

    public override void ResetDirectable() {
        isActive = false;
        isCompleted = false;
    }

    public override void ResetProgress()
    {
        // shud also have nothing
        // only eliminationQuest will override it
    }
    #endregion


    #region CurrStateData
    public override void SetCompletion(bool completion)
    {
        isCompleted = completion;
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