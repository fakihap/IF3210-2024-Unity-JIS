using UnityEngine;
using System.Collections.Generic;

public abstract class QuestNotifier : MonoBehaviour {
    [SerializeField]
    private List<Quest> subscribers;
    protected abstract void CheckQuest();
    public void Subscribe(Quest quest) {
        subscribers.Add(quest);
    }
    public void Unsubscribe(Quest quest) {
        subscribers.Remove(quest);
    }

    // should be called from CheckQuest
    public void Notify() {
        foreach (Quest quest in subscribers)
        {
            quest.ProgressQuest();
        }
    }
    void Update() {
        CheckQuest();
    }
}