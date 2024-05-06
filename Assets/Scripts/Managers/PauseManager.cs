using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	public static bool isPaused;
	
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
		Time.timeScale= 1;
		canvas.enabled = false;
		isPaused = false;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			canvas.enabled = !canvas.enabled;
			Pause();
		}
	}
	
	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		isPaused = Time.timeScale == 0;
		Lowpass ();
		
	}

	public static void StaticPauseOrUnPause()
    {
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		isPaused = Time.timeScale == 0;
	}
	
	void Lowpass()
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
