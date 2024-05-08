using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : MonoBehaviour
{
	public Button quitButton;

	void Start()
	{
		quitButton.onClick.AddListener(Exit);
	}

	void Exit()
	{
		print("exit");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
