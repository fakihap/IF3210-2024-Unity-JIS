using UnityEngine;

public class DefeatQuestNotifier : QuestNotifier
{
    protected override void CheckQuest()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            Notify();
        }
    }
}