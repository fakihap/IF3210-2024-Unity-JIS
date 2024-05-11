using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button checkpointButton;
    public Button mainMenuButton;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToCheckpoint()
    {
        string currFileName = "Slot";
        currFileName += CurrStateData.currGameData.currentSlot.ToString() + ".dat";

        string output;
        if (FileManager.LoadFromFile(currFileName, out output))
        {
            print(currFileName + " output: " + output);
            CurrStateData.LoadFromJson(output);
            CurrStateData.currGameData.currentSlot = 1;
        }

        SceneManager.LoadScene("Stage01");
    }
}
