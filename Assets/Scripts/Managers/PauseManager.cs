﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{

	public static AudioMixerSnapshot paused;
	public static AudioMixerSnapshot unpaused;
	public static bool isPaused;

	public Canvas healthCanvas;
	public Canvas pauseCanvas;

	void Start()
	{
		Time.timeScale = 1;
		// canvas.enabled = false;
		pauseCanvas.enabled = false;
		healthCanvas.enabled = true;
		isPaused = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
			healthCanvas.enabled = !isPaused;
			pauseCanvas.enabled = isPaused;
		}
	}

	public static void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		isPaused = Time.timeScale == 0;
		Lowpass();
		if (isPaused) {
			float currentTime = Time.time;
			float elapsedTimePaused = currentTime - CurrStateData.currGameData.startTime;
			CurrStateData.currGameData.elapsedTime += elapsedTimePaused; 
			CurrStateData.currGameData.startTime = 0;

			float savedElapsedTime = PlayerPrefs.GetFloat("elapsedTime");
      // print(savedElapsedTime);
      PlayerPrefs.SetFloat("elapsedTime", savedElapsedTime + elapsedTimePaused);
		} else {
			CurrStateData.currGameData.startTime = Time.time;
		}
	}

	public static void StaticPauseOrUnPause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		isPaused = Time.timeScale == 0;
	}

	public static void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			if (paused != null)
			{
				paused.TransitionTo(.01f);
			}
		}
		else

		{
			if (unpaused != null)
			{
				unpaused?.TransitionTo(.01f);
			}
		}
	}

	public void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public static bool IsPaused()
	{
		return isPaused;
	}
}
