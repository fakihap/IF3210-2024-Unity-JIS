using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TMP_InputField playerName;
	public TextMeshProUGUI volumeInfo;
    public TMP_Dropdown difficultyDropdown;
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("playerName");
        
        int volume = PlayerPrefs.GetInt("volume");
        volumeInfo.text = volume.ToString() + "%";
        AudioListener.volume = (float)volume / 100;
        difficultyDropdown.value = PlayerPrefs.GetInt("difficultyLevelIndex");
    }

    public void VolumeUp()
    {
        int volume = PlayerPrefs.GetInt("volume");
        volume += 5;
        if (volume > 100) return;
        PlayerPrefs.SetInt("volume", volume);
        AudioListener.volume = (float) volume / 100;
        Debug.Log("Volume Up: " + volume);
        volumeInfo.text = volume.ToString() + "%";
    }

    public void VolumeDown()
    {
        int volume = PlayerPrefs.GetInt("volume");
        volume -= 5;
        if (volume < 0) return;
        PlayerPrefs.SetInt("volume", volume);
        Debug.Log("Volume Up: " + volume);
        volumeInfo.text = volume.ToString() + "%";
    }

    public void ChangeName()
    {
        if (playerName.text.Length > 16) return;
        PlayerPrefs.SetString("playerName", playerName.text);
        Debug.Log("Ganti nama: " + playerName.text);
    }

    public void ChangeDifficulty()
    {
        Debug.Log("Difficulty dropdown index: " + difficultyDropdown.value);
        int difficultyIndex = difficultyDropdown.value;
        PlayerPrefs.SetInt("difficultyLevelIndex", difficultyIndex);
    }
}