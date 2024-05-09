using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : MonoBehaviour
{
	public Button statisticsButton;
	public Button closeStatisticsButton;
	public Button settingsButton;
	public Button quitButton;
	public Canvas StatisticPage;
	public StatsManager statsManager;

	void Start()
	{
		quitButton.onClick.AddListener(Exit);
		statisticsButton.onClick.AddListener(ShowStatistics);
		closeStatisticsButton.onClick.AddListener(CloseStatistics);
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

		void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StatisticPage.enabled = false;
		}
	}
}
