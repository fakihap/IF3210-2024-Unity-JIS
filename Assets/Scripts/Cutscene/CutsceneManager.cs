using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        mainCamera = Camera.main;
        DisableUI();
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P) && !isInCutscene) {
    //         StartCutscene();
    //     }
    // }

    void StartCutscene(Cutscene cutscene) {
        currentCutscene = cutscene;

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
 