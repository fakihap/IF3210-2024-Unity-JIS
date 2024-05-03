using UnityEngine;

public class SurviveQuest : Quest
{
    [SerializeField]
    float surviveDuration = 5f; // in seconds
    private bool isFinished = false;

    protected override void StartQuest() {
        isFinished = false;
    }
    public override void ProgressQuest()
    {
        isFinished = true;
    }
    
    public override string GetQuestMessage()
    {
        string durationString = "";

        if (surviveDuration > 60) {
            durationString = string.Format("{0} minute(s) {1} second(s)", (int)surviveDuration / 60, (int)surviveDuration % 60);
        } else {
            durationString = string.Format("{0} second(s)", (int)surviveDuration);
        }

        return string.Format("Survive for {0}", durationString);
    }

    // this called each update
    protected override bool CheckQuest()
    {
        // we dont use notifier for this quest, we use internal update instead
        if (!isFinished && isActive) {
            surviveDuration -= Time.deltaTime;
            
            questManager.UpdateUI();
        }

        if (surviveDuration <= 0f) {
            ProgressQuest();
        }
        
        return isFinished;
    }
}