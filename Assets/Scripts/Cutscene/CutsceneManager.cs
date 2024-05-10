using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    Cutscene currentCutscene;

    [SerializeField]
    Camera mainCamera;

    [Header("UI Elements")]
    [SerializeField]
    private Image bgImage;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;


    [Header("Dialogues")]
    

    [SerializeField]
    private bool isInCutscene = false;

    void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded: " + scene.name);
        
        mainCamera = Camera.main;

        // these needs the name used to be the same
        try 
        {
            bgImage = GameObject.Find("DialogueBG").GetComponent<Image>();
            nameText = GameObject.Find("DialogueName").GetComponent<TextMeshProUGUI>();
            dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();

            DisableUI();

            StartCutscene();
        }
        catch (Exception e)
        {
            Debug.LogWarning("CutsceneManager error : " + e.Message);
        }        
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetCurrentCutscene(Cutscene cutscene) {
        currentCutscene = cutscene;
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P) && !isInCutscene) {
    //         StartCutscene();
    //     }
    // }

    public void StartCutscene() {
        isInCutscene = true;
        EnableUI();

        ProgressCutscene();
            

        // check this behavior later
        // EndCutscene();
    }

    public void ProgressCutscene() {
        // if cutscene is finished
        if (!currentCutscene.NextDialogue()) {
            isInCutscene = false;
            DisableUI();
            
            EndCutscene();

            return;
        }

        Dialogue currentDialogue = currentCutscene.GetCurrentDialogue();

        nameText.text = currentDialogue.name;
        dialogueText.text = currentDialogue.text;
        mainCamera.transform.position = currentDialogue.camTransform.position;
        mainCamera.transform.rotation = Quaternion.Euler(currentDialogue.camTransform.rotation);
    }

    void EndCutscene() {
        // currently using build index, might cause progress corruption
        // load next scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentCutscene.EndCutscene();
        GameDirector.Instance.UpdateDirector();
    }


    void EnableUI() {
        bgImage.enabled = true;

        nameText.enabled = true;
        dialogueText.enabled = true;
    }

    void DisableUI() {
        bgImage.enabled = false;

        nameText.enabled = false;
        dialogueText.enabled = false;
    }
}
 