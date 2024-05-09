using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class QuestNotifier : MonoBehaviour {
    [SerializeField]
    private List<Quest> subscribers;
    protected abstract void SetSubscriber(List<Quest> quests);
    protected void Subscribe(Quest quest) {
        subscribers.Add(quest);
    }
    protected void Unsubscribe(Quest quest) {
        subscribers.Remove(quest);
    }

    void Start() {
        Debug.Log("NOTIFA " + GameDirector.Instance.GetQuests().Count);
        SetSubscriber(GameDirector.Instance.GetQuests());
    }

    // should be called from CheckQuest
    protected void Notify() {
        foreach (Quest quest in subscribers)
        {
            quest.ProgressQuest();
        }
    }
}