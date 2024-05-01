using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void settingsButton()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void backButton()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void exitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
