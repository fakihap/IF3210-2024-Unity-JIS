using UnityEngine;

[CreateAssetMenu(fileName = "Tr_Quest_01", menuName = "Directable Objects/New Travel Quest", order = 1)]
public class TravelQuest : Quest
{
    public string targetName = "Default-Target-Name"; // this need to be unique

    protected override void StartQuest() {
        isCompleted = false;
    }
    public override void ProgressQuest()
    {
        isCompleted = true;
    }
    
    public override string GetQuestMessage()
    {
        return string.Format("Go to somewhere {0}", targetName);
    }
}