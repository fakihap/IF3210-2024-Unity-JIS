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

    }
}
