using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : MonoBehaviour
{
	public Button statisticsButton;
	public Button closeStatisticsButton;
	public Button settingsButton;
	public Button closeSettingsButton;
	public Button quitButton;
	public Canvas StatisticPage;
	public Canvas SettingPage;
	public StatsManager statsManager;
	public SettingsManager settingsManager;
	public CloudProjectSettingsEventManager settingManager;

	void Start()
	{
		quitButton.onClick.AddListener(Exit);
		statisticsButton.onClick.AddListener(ShowStatistics);
		settingsButton.onClick.AddListener(ShowSettings);
		closeStatisticsButton.onClick.AddListener(CloseStatistics);
		closeSettingsButton.onClick.AddListener(CloseSettings);
	}

	void Exit()
	{
		print("exit");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	void ShowStatistics()
	{
		StatisticPage.enabled = true;
		statsManager.UpdateStats();
	}

	void CloseStatistics()
	{
		StatisticPage.enabled = false;
	}

	void CloseSettings()
	{
		SettingPage.enabled = false;
	}

	void ShowSettings()
	{
		SettingPage.enabled = true;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StatisticPage.enabled = false;
			SettingPage.enabled = false;
		}
	}
}
