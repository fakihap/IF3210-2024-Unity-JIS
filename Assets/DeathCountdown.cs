using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCountdown : MonoBehaviour
{
    private Text _text;
    private bool _timerRunning = false;
    private float _startTime = 0f;

    private void Awake()
    {
        _text = GetComponent<Text>();
        StartTimer();
    }
    
    private void Update()
    {
        float currentTime = Time.time;
        float elapsedTime = currentTime - _startTime; 


        if (_timerRunning && elapsedTime <= 15)
        {
            // Debug.Log("Playtime: " + elapsedTime);
            UpdateTimerDisplay(15 - elapsedTime);
        } else
        {
            StopTimer();
        }
    }

    private void StartTimer()
    {
        _startTime = Time.time;
        _timerRunning = true;
    }

    private void StopTimer()
    {
        _timerRunning = false;
    }

    private void UpdateTimerDisplay(float elapsedTime)
    {
        var minutes = Mathf.FloorToInt(elapsedTime / 60);
        var seconds = Mathf.FloorToInt(elapsedTime % 60);
        _text.text = $"{minutes:00}:{seconds:00}";
    }
}
