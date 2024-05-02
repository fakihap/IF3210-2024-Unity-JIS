using UnityEngine;
using System.Collections.Generic;

public abstract class QuestNotifier : MonoBehaviour {
    [SerializeField]
    private List<Quest> subscribers;
    public void Subscribe(Quest quest) {
        subscribers.Add(quest);
    }
    public void Unsubscribe(Quest quest) {
        subscribers.Remove(quest);
    }

    // should be called from CheckQuest
    protected void Notify() {
        foreach (Quest quest in subscribers)
        {
            quest.ProgressQuest();
        }
    }
}