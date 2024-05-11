using UnityEngine;

[CreateAssetMenu(fileName = "Su_Quest_01", menuName = "Directable Objects/New Survive Quest", order = 1)]
public class SurviveQuest : Quest
{
    [SerializeField]
    float surviveDuration = 5f; // in seconds

    float currentSurviveDuration = 0f;
    protected override void StartQuest() {
        isCompleted = false;
    }
    public override bool ProgressQuest()
    {
        if (!base.ProgressQuest()) {
            return false;
        }
        
        isCompleted = true;

        return true;
    }
    
    public override string GetQuestMessage()
    {
        string durationString = "";

        if (currentSurviveDuration > 60) {
            durationString = string.Format("{0} minute(s) {1} second(s)", (int)currentSurviveDuration / 60, (int)currentSurviveDuration % 60);
        } else {
            durationString = string.Format("{0} second(s)", (int)currentSurviveDuration);
        }

        return string.Format("Survive for {0}", durationString);
    }

    // this called each update
    public override bool UpdateQuest()
    {
        if (!base.UpdateQuest()) {
            return false;
        }
        
        
        // we dont use notifier for this quest, we use internal update instead
        if (!IsCompleted() && IsActive()) {
            currentSurviveDuration -= Time.deltaTime;
            
            QuestManager.Instance.UpdateUI();
        }

        if (currentSurviveDuration <= 0f) {
            return ProgressQuest(); // blum cek behavior ini
        }

        return true;
    }

    public override void ResetDirectable()
    {
        base.ResetDirectable();

        currentSurviveDuration = surviveDuration;
    }

    public override void ResetProgress()
    {
        base.ResetProgress();

        currentSurviveDuration = surviveDuration;
    }
}