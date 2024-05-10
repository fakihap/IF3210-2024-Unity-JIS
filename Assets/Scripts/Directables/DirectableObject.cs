using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DirectableObject : ScriptableObject
{
   [SerializeField] List<DirectableObject> precedingDirectables;

    public abstract bool IsActive();
    public abstract bool IsCompleted();
    protected abstract void StartDirectable(); 
    protected abstract void EndDirectable();
    public abstract void ResetDirectable(); // later fix this
    
    public void UpdateDirectable() {
        // Debug.Log("UPDATING THIS DIRECTABLE " + name);
    
        if (CheckEnd()) {
            return;
        }


        // check this, dont know why we needde it
        if (IsActive()) {
            return;
        }

        CheckStart();
    }

    bool CheckEnd() {
        // end if not ended
        if (IsCompleted()) {
            if (IsActive()) {
                // end only if active
                EndDirectable();
            }
            
            return true;
        }

        return false;
    }

    bool CheckStart() {
        // start if not yet started AND all preceding DO completed
        // check if all preceding directables are completed
        foreach (DirectableObject precedingObject in precedingDirectables) {
            if (!precedingObject.IsCompleted()) {
                return false;
            }
        }

        // Debug.Log("CALLING SD OPN THIS DIRECTABLE");
        StartDirectable();
        return true;
    }

# region CurrStateData
    public abstract void SetCompletion(bool completion);
# endregion
}
