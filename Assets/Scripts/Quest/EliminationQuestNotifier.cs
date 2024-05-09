using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Nightmare {
public class EliminationQuestNotifier : QuestNotifier
{
    [SerializeField] EnemyType enemyType;

    // notify that the target is dead
    public void NotifyElimination()
    {
        Notify();
    }

    protected override void SetSubscriber(List<Quest> quests)
    {
        foreach (EliminationQuest quest in quests.Where(x => x.GetType() == typeof(EliminationQuest)).Cast<EliminationQuest>().ToList())
        {
            if (quest.enemyType != enemyType)
            {
                continue;
            }

            Subscribe(quest);
        }
    }
}
}
