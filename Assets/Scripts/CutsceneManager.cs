using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
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
    private int cutsceneDialoguesIndex = -1;             // index for which cutscene
    [SerializeField]
    private List<CutsceneDialog> cutsceneDialogues;

    [SerializeField]
    private bool isInCutscene = false;

    [SerializeField]
    private int dialoguesIndex = -1, dialoguesCount;

    void Start()
    {
        mainCamera = Camera.main;
        DisableUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isInCutscene) {
            StartCutscene();
        }
    }

    void StartCutscene() {
        if (cutsceneDialoguesIndex + 1 < cutsceneDialogues.Count) {
            cutsceneDialoguesIndex += 1;

            isInCutscene = true;

            dialoguesIndex = -1;
            dialoguesCount = cutsceneDialogues[cutsceneDialoguesIndex].dialogues.Count;

            ProgressCutscene();

            isInCutscene = true;
            EnableUI();
            return;
        }

        EndCutscene();
    }

    public void ProgressCutscene() {
        if (dialoguesIndex + 1 == dialoguesCount) {
            isInCutscene = false;
            DisableUI();
            return;
        }

        dialoguesIndex += 1;

        Dialog currentDialog = cutsceneDialogues[cutsceneDialoguesIndex].dialogues[dialoguesIndex];
        nameText.text = currentDialog.name;
        dialogueText.text = currentDialog.text;
        mainCamera.transform.position = currentDialog.camTransform.position;
        mainCamera.transform.rotation = Quaternion.Euler(currentDialog.camTransform.rotation);
    }

    void EndCutscene() {
        // currently using build index, might cause progress corruption
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
 