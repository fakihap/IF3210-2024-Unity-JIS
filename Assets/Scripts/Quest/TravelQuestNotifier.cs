using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TravelQuestNotifier : QuestNotifier
{
    [SerializeField]
    string targetName = "Default-Target-Name"; // kinda needs to be unique
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            // target reached
            Notify();
        }
    }

    protected override void SetSubscriber(List<Quest> quests)
    {
        foreach (TravelQuest quest in quests.Where(x => x.GetType() == typeof(TravelQuest)).Cast<TravelQuest>().ToList()) {
            if (quest.targetName != targetName) {
                continue;
            }
            
            Subscribe(quest);
            // was putting break here, do we need to have single target only?
        }
    }
}