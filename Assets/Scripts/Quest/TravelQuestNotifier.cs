using UnityEngine;

public class TravelQuestNotifier : QuestNotifier
{
    protected override void CheckQuest()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Notify();
        }
    }
}