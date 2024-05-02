using Unity.VisualScripting;
using UnityEngine;

public class DefeatQuestNotifier : QuestNotifier
{
    // notify that the target is dead
    public void NotifyDefeat()
    {
        Notify();
    }
}