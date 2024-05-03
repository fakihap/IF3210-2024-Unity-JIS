using UnityEngine;

public class DefeatQuest : Quest
{
    [SerializeField]
    int currentCount = 0, targetCount;
    protected override void StartQuest()
    {
        targetCount = notifiers.Count;
    }
    public override void ProgressQuest()
    {
        currentCount += 1;

        Debug.Log("Quest Updated : " + GetQuestMessage());
        
        questManager.UpdateUI();
    }
    
    public override string GetQuestMessage()
    {
        return string.Format("Defeat enemies : {0} of {1}", currentCount, targetCount);
    }

    protected override bool CheckQuest()
    {
        return currentCount == targetCount;
    }
}