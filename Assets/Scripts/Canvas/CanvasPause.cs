using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPause : MonoBehaviour
{
	public Button quitButton;

	void Start()
	{
		quitButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Application.Quit();
	}
}
