using UnityEngine;

public class TravelQuest : Quest
{
    [SerializeField]
    string targetName = "Default-Target-Name";
    [SerializeField]
    bool isReached = false;

    protected override void StartQuest() {
        isReached = false;
    }
    public override void ProgressQuest()
    {
        isReached = true;
    }
    
    public override string GetQuestMessage()
    {
        return string.Format("Go to somewhere {0}", targetName);
    }

    protected override bool CheckQuest()
    {
        return isReached;
    }
}