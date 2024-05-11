using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public Button exitButton;
    public Button statsButton;
    public Button settingsButton;
    public Button slot1Button;
    public Button slot2Button;
    public Button slot3Button;
    public StatsManagerMainMenu statsManager;
    public SettingsManager settingsManager;
    public Button closeSettingsButton;
    public Button closeStatisticButton;
    public Button closeLoadGameButton;
    public GameObject MainMenuPanel;
    public GameObject SettingsPage;
    public GameObject LoadPanel;
    public GameObject StatisticsPanel;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(Exit);
		statsButton.onClick.AddListener(ShowStatistics);
		settingsButton.onClick.AddListener(ShowSettings);
		closeStatisticButton.onClick.AddListener(CloseStatistics);
		closeSettingsButton.onClick.AddListener(CloseSettings);
        loadGameButton.onClick.AddListener(ShowLoadGame);
        closeLoadGameButton.onClick.AddListener(CloseLoadGame);
        MainMenuPanel.SetActive(true);
        SettingsPage.SetActive(false);
        LoadPanel.SetActive(false);
        StatisticsPanel.SetActive(false);
    }

    void Exit()
	{
		print("exit");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

    public void NewGame() {

        // By default, this overwrites slot 
        CurrStateData.InstantiateNewData();
        SceneManager.LoadScene("Stage01");
    }

    public void LoadSlot1()
    {
        string output;
        if (FileManager.LoadFromFile("Slot1.dat", out output))
        {
            print("Slot 1 output" + output);
            CurrStateData.LoadFromJson(output);
            CurrStateData.currGameData.currentSlot = 1;
        } else
        {
            print("File doesn't exist");
        }
        SceneManager.LoadScene("Stage01");
    }

    public void LoadSlot2()
    {
        string output;
        if (FileManager.LoadFromFile("Slot2.dat", out output))
        {
            print("Slot 2 output" + output);
            CurrStateData.LoadFromJson(output);
            CurrStateData.currGameData.currentSlot = 2;
        } else
        {
            print("File doesn't exist");
        }
        SceneManager.LoadScene("Stage01");
    }

    public void LoadSlot3()
    {
        string output;
        if (FileManager.LoadFromFile("Slot3.dat", out output))
        {
            print("Slot 3 output" + output);
            CurrStateData.LoadFromJson(output);
            CurrStateData.currGameData.currentSlot = 3;
        } else
        {
            print("File doesn't exist");
        }
        SceneManager.LoadScene("Stage01");
    }

    void ShowLoadGame()
    {
        MainMenuPanel.SetActive(false);
        LoadPanel.SetActive(true);
    }

    void CloseLoadGame()
    {
        MainMenuPanel.SetActive(true);
        LoadPanel.SetActive(false);
    }

    void ShowStatistics()
	{
		MainMenuPanel.SetActive(false);
        StatisticsPanel.SetActive(true);
		statsManager.UpdateStats();
	}

    void CloseStatistics()
	{
		MainMenuPanel.SetActive(true);
        StatisticsPanel.SetActive(false);
	}

    void CloseSettings()
	{
        MainMenuPanel.SetActive(true);
        SettingsPage.SetActive(false);
	}

	void ShowSettings()
	{
        MainMenuPanel.SetActive(false);
        SettingsPage.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
