using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }

    void Update()
    {
        bgImage.enabled = isInCutscene;

        nameText.enabled = isInCutscene;
        dialogueText.enabled = isInCutscene;

        if (Input.GetKeyDown(KeyCode.P)) {
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
        }
    }

    public void ProgressCutscene() {
        if (dialoguesIndex + 1 == dialoguesCount) {
            isInCutscene = false;
            return;
        }

        dialoguesIndex += 1;

        Dialog currentDialog = cutsceneDialogues[cutsceneDialoguesIndex].dialogues[dialoguesIndex];
        nameText.text = currentDialog.name;
        dialogueText.text = currentDialog.text;
        mainCamera.transform.position = currentDialog.camTransform.position;
        mainCamera.transform.rotation = currentDialog.camTransform. rotation;
    }
}
 