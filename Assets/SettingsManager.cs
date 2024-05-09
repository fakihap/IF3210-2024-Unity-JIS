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
        playerName.text = CurrStateData.GetPlayerName();
        volumeInfo.text = CurrStateData.GetVolume() + "%";
        AudioListener.volume = (float)CurrStateData.GetVolume() / 100;
        difficultyDropdown.value = CurrStateData.GetDifficultyLevelIndex();
    }

    public void VolumeUp()
    {
        CurrStateData.ChangeVolume(5);
        AudioListener.volume = (float) CurrStateData.GetVolume() / 100;
        Debug.Log("Volume Up: " + CurrStateData.GetVolume());
        volumeInfo.text = CurrStateData.GetVolume() + "%";
    }

    public void VolumeDown()
    {
        CurrStateData.ChangeVolume(-5);
        AudioListener.volume = (float) CurrStateData.GetVolume() / 100;
        Debug.Log("Volume Down: " + CurrStateData.GetVolume());
        volumeInfo.text = CurrStateData.GetVolume() + "%";
    }

    public void ChangeName()
    {
        CurrStateData.ChangePlayerName(playerName.text);
        Debug.Log("Ganti nama: " + CurrStateData.GetPlayerName());
    }

    public void ChangeDifficulty()
    {
        Debug.Log("Difficulty dropdown index: " + difficultyDropdown.value);
        int difficultyIndex = difficultyDropdown.value;
        string difficulty;
        if(difficultyIndex == 0)
        {
            difficulty = "easy";
        }
        else if(difficultyIndex == 1)
        {
            difficulty = "medium";
        }
        else
        {
            difficulty = "hard";
        }
        CurrStateData.SetDifficultyLevel(difficulty);
        Debug.Log("CurrState Difficulty: " + CurrStateData.GetDifficultyLevel());
    }
}