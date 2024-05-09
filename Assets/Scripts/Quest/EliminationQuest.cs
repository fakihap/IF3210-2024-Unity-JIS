using UnityEngine;

public enum EnemyType {
    Keroco,
    KepalaKeroco,
    Jenderal,
    Raja
}

[CreateAssetMenu(fileName = "El_Quest_01", menuName = "Directable Objects/New Elimination Quest", order = 1)]
public class EliminationQuest : Quest
{
    

    int currentCount = 0;
    [SerializeField] int targetCount = 1; // get this from SO data
    public EnemyType enemyType;

    protected override void StartQuest()
    {
        isCompleted = false;
    }
    public override bool ProgressQuest()
    {
        if (!base.ProgressQuest()) {
            return false;
        }

        currentCount += 1;

        Debug.Log("Quest Updated : " + GetQuestMessage());

        // need helepr
        QuestManager.Instance.UpdateUI();

        return true;
    }
    
    public override string GetQuestMessage()
    {
        return string.Format("Defeat enemies : {0} of {1}", currentCount, targetCount);
    }

    public override bool UpdateQuest()
    {
        if (!base.UpdateQuest()) {
            return false;
        }

        if (currentCount >= targetCount) {
            isCompleted = true;
        }

        return true;
    }

    public override void ResetDirectable()
    {
        base.ResetDirectable();


        // resetting to default values
        currentCount = 0;
        targetCount = 1; // get this from SO data
    }
}