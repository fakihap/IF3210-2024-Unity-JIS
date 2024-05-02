using System;
using UnityEngine;

public class TravelQuestNotifier : QuestNotifier
{
    private bool isReached = false;
    protected override void CheckQuest()
    {
        if (isReached) {
            Notify();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            // target reached
            isReached = true;
        }
    }
}