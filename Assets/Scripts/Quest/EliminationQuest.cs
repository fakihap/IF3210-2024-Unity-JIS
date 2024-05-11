using UnityEngine;

public enum EnemyType {
    Any,
    Keroco,
    KepalaKeroco,
    Jenderal,
    Raja
}

[CreateAssetMenu(fileName = "El_Quest_01", menuName = "Directable Objects/New Elimination Quest", order = 1)]
public class EliminationQuest : Quest
{
    

    int currentCount = 0, currentTargetCount = 1;
    [SerializeField] int targetCount = 1; // get this from SO data
    public EnemyType enemyType;

    protected override void StartQuest()
    {
        isCompleted = false;
        currentCount = 0;
        currentTargetCount = targetCount;

        // WARNING : this dirty code will only works on this edge case
        //           we do it this way since we are short on time KWWKKWWKKWKWKW
        if (enemyType == EnemyType.Raja) {
            GameDirector.Instance.isBossReady = true;
        }
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
        string enemyDesc = enemyType == EnemyType.Any ? "any enemies" : enemyType.ToString();
        return string.Format("Defeat {2} : {0} of {1}", currentCount, currentTargetCount, enemyDesc);
    }

    public override bool UpdateQuest()
    {
        if (!base.UpdateQuest()) {
            return false;
        }

        if (currentCount >= currentTargetCount) {
            isCompleted = true;
        }

        return true;
    }

    public override void ResetDirectable()
    {
        base.ResetDirectable();


        // resetting to default values
        currentCount = 0;
        currentTargetCount = 1; // get this from SO data
    }

    // the only directable who needs this method
    // need to recheck this
    public override void ResetProgress()
    {
        currentCount = 0;
        
        base.ResetProgress();
    }
}